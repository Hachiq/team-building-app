import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Team } from 'src/app/models/team';
import { User } from 'src/app/models/user';
import { RequestService } from 'src/app/services/request.service';
import { TeamService } from 'src/app/services/team.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-team-profile',
  templateUrl: './team-profile.component.html',
  styleUrl: './team-profile.component.scss'
})
export class TeamProfileComponent {
  teamId!: number;

  team!: Team;
  users!: User[];

  displayedColumns: string[] = [ 'id', 'username', 'firstName', 'lastName', 'email'];

  constructor (private route: ActivatedRoute, private router: Router, private teamService: TeamService, private requestService: RequestService, private tokenService: TokenService) {
    route.params.subscribe(params => {
      this.teamId = +params['id']; // Convert to number
    });
  }

  ngOnInit(){
    this.loadTeam(this.teamId);
    this.loadUsers(this.teamId);
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

  canViewRequests(){
    return this.tokenService.getTeamIdFromToken() == this.team.id;
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
      .subscribe((result) => this.users = result);
  }
}
