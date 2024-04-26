import { DatePipe } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
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
  dataSource!: MatTableDataSource<Request>;

  displayedColumns: string[] = [ 'username', 'date', 'status', 'action'];

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private route: ActivatedRoute, private teamService: TeamService, private requestService: RequestService, public datePipe: DatePipe) {
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
      .subscribe(() => {
        this.loadRequests(this.teamId);
      },
      (error) => {
        console.log(error.error);  
      }
    );
  }

  decline(id: number){
    this.requestService
      .decline(id)
      .subscribe(() => {
        this.loadRequests(this.teamId);
      },
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
      .subscribe((result) => {
        this.dataSource = new MatTableDataSource<Request>(result);
        this.dataSource.paginator = this.paginator;
      }
    );
  }
}
