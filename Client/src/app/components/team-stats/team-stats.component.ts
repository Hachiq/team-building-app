import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TeamStats } from 'src/app/models/teamStats';
import { StatsService } from 'src/app/services/stats.service';

@Component({
  selector: 'app-team-stats',
  templateUrl: './team-stats.component.html',
  styleUrl: './team-stats.component.scss'
})
export class TeamStatsComponent {
  teamId!: number;
  teamStats!: TeamStats;

  constructor (private route: ActivatedRoute, private statsService: StatsService) {
    route.params.subscribe(params => {
      this.teamId = +params['id']; // Convert to number
    });
  }

  ngOnInit(){
    this.loadTeamStats();
  }

  loadTeamStats() {
    this.statsService
    .getByTeamId(this.teamId)
    .subscribe((result) => this.teamStats = result);
  }
}
