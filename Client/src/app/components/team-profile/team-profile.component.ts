import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Team } from 'src/app/models/team';
import { User } from 'src/app/models/user';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-team-profile',
  templateUrl: './team-profile.component.html',
  styleUrl: './team-profile.component.scss'
})
export class TeamProfileComponent {
  teamId!: number;

  team!: Team;
  users!: User[];

  constructor (private route: ActivatedRoute, private teamService: TeamService) {
    route.params.subscribe(params => {
      this.teamId = +params['id']; // Convert to number
    });
  }

  ngOnInit(){
    this.loadTeam(this.teamId);
    this.loadUsers(this.teamId);
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
