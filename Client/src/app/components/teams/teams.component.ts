import { Component } from '@angular/core';
import { DisplayTeam } from 'src/app/models/displayTeam';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrl: './teams.component.scss'
})
export class TeamsComponent {
  teams?: DisplayTeam[];

  constructor(private teamService: TeamService) { }

  ngOnInit() {
    this.loadTeams();
  }

  loadTeams() {
    this.teamService
      .get()
      .subscribe((result) => this.teams = result)
  }
}
