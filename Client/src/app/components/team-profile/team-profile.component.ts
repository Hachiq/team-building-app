import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-team-profile',
  templateUrl: './team-profile.component.html',
  styleUrl: './team-profile.component.scss'
})
export class TeamProfileComponent {
  teamId!: number;

  constructor (private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.teamId = +params['id']; // Convert to number
    });
  }
}
