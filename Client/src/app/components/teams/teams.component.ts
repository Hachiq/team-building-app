import { Component } from '@angular/core';
import { Team } from 'src/app/models/team';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrl: './teams.component.scss'
})
export class TeamsComponent {
  teams!: Team[];
  displayedColumns: string[] = [ 'name', 'numberOfMembers'];

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
