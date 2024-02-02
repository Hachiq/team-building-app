import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Request } from 'src/app/models/request';
import { Team } from 'src/app/models/team';
import { RequestService } from 'src/app/services/request.service';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrl: './requests.component.scss'
})
export class RequestsComponent {
  teamId!: number;

  team!: Team;
  requests!: Request[];

  displayedColumns: string[] = [ 'username', 'date', 'status', 'action'];

  constructor(private route: ActivatedRoute, private teamService: TeamService, private requestService: RequestService) {
    route.params.subscribe(params => {
      this.teamId = +params['id']; // Convert to number
    });
  }

  ngOnInit(){
    this.loadTeam(this.teamId);
    this.loadRequests(this.teamId);
  }

  accept(id: number){
    this.requestService
      .accept(id)
      .subscribe(() => {},
        (error) => {
          console.log(error.error);  
        }
      );
  }

  decline(id: number){
    this.requestService
      .decline(id)
      .subscribe(() => {},
        (error) => {
          console.log(error.error);
        }
      );
  }

  loadTeam(id: number){
    this.teamService
      .getById(id)
      .subscribe((result) => this.team = result);
  }

  loadRequests(id: number){
    this.requestService
      .get(id)
      .subscribe((result) => this.requests = result);
  }
}
