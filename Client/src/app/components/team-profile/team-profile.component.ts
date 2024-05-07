import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SelectionModel } from '@angular/cdk/collections';
import { Team } from 'src/app/models/team';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { RequestService } from 'src/app/services/request.service';
import { TeamService } from 'src/app/services/team.service';
import { TokenService } from 'src/app/services/token.service';
import { StatsService } from 'src/app/services/stats.service';
import { TeamMember } from 'src/app/models/teamMember';
import { TeamStatsComponent } from '../team-stats/team-stats.component';

@Component({
  selector: 'app-team-profile',
  templateUrl: './team-profile.component.html',
  styleUrl: './team-profile.component.scss'
})
export class TeamProfileComponent {
  teamId!: number;

  team!: Team;
  dataSource!: MatTableDataSource<TeamMember>;
  selection = new SelectionModel<TeamMember>(true, []);

  displayedColumns: string[] = [ 'select', 'username', 'firstName', 'lastName', 'salary', 'debt', 'isLeader'];

  @ViewChild(MatSort) sort!: MatSort;

  @ViewChild(TeamStatsComponent) childComponent: TeamStatsComponent | undefined;

  constructor (private route: ActivatedRoute, private router: Router, private teamService: TeamService, private requestService: RequestService, private tokenService: TokenService, private statsService: StatsService) {
    route.params.subscribe(params => {
      this.teamId = +params['id']; // Convert to number
    });
  }

  ngOnInit(){
    this.loadTeam(this.teamId);
    this.loadUsers(this.teamId);
    this.router.routeReuseStrategy.shouldReuseRoute = () => { return false; };
  }

  createRequest(){
    this.requestService.create({
      userId: this.tokenService.getUserIdFromToken(),
      teamId: this.team.id 
    }).subscribe(() => {},
      (error) => {
        if (error.status === 401) {
          this.router.navigate(["login"]);
        }
        console.log(error.error);
      }
    )
  }

  isTeamMember(){
    if(this.team){
      return this.tokenService.getTeamIdFromToken() == this.team.id;
    }
    return false;
  }

  canDisband(){
    if(this.team){
      return (this.tokenService.getTeamIdFromToken() == this.team.id) && this.tokenService.userIsLeader();
    }
    return false;
  }

  canLeave(){
    if(this.team){
      return (this.tokenService.getTeamIdFromToken() == this.team.id) && !this.tokenService.userIsLeader();
    }
    return false;
  }

  canAddDays(){
    if(this.team){
      return (this.tokenService.getTeamIdFromToken() == this.team.id) && this.tokenService.userIsLeader();
    }
    return false;
  }

  canJoin() {
    return !this.tokenService.getTeamIdFromToken();
  }

  loadTeam(id: number){
    this.teamService
      .getById(id)
      .subscribe((result) => this.team = result);
  }
  loadUsers(id: number){
    this.teamService
      .getUsersByTeamId(id)
      .subscribe((result) => {
        this.dataSource = new MatTableDataSource<TeamMember>(result);
        if (this.isTeamMember()){
          this.dataSource.sort = this.sort;
        }
      }
    );
  }

  disband(){
    this.teamService.disband(this.team.id).subscribe(() => {
      this.router.navigate(['teams'])
    });
  }

  leave(){
    this.teamService.leave(this.team.id, this.tokenService.getUserIdFromToken()).subscribe(() => {
      this.router.navigate(['teams'])
    })
  }

  goToUserProfile(id: number) {
    if (this.tokenService.getTeamIdFromToken() == this.team.id && 
          (this.tokenService.userIsLeader() || this.tokenService.getUserIdFromToken() == id)) {
      this.router.navigate(["user-profile", id]);
    }
  }

  addDayWorked(){
    if(!this.canAddDays) return;

    const selectedUsers = this.getSelectedUsers();
    selectedUsers.forEach(user => {
      this.statsService.addDayWorked(user.id).subscribe();
    })

    this.childComponent?.loadTeamStats();
  }

  addDayPaid(){
    if(!this.canAddDays) return;

    const selectedUsers = this.getSelectedUsers();
    selectedUsers.forEach(user => {
      this.statsService.addDayPaid(user.id).subscribe();
    })

    this.childComponent?.loadTeamStats();
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.dataSource.data);
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: TeamMember): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id + 1}`;
  }

  getSelectedUsers() {
    return this.selection.selected;
  }
}
