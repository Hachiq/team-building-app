import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Team } from 'src/app/models/team';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrl: './teams.component.scss'
})
export class TeamsComponent {
  teams!: Team[];
  displayedColumns: string[] = [ 'name', 'leader', 'numberOfMembers'];

  constructor(private teamService: TeamService, private router: Router) { }

  ngOnInit() {
    this.loadTeams();
  }

  loadTeams() {
    this.teamService
      .get()
      .subscribe((result) => this.teams = result)
  }
}
