import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Team } from '../models/team';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { CreateTeam } from '../interfaces/create.team';
import { User } from '../models/user';
import { TeamMember } from '../models/teamMember';

@Injectable({
  providedIn: 'root'
})
export class TeamService {

  constructor(private http: HttpClient) { }

  public get(): Observable<Team[]> {
    return this.http.get<Team[]>(
      `${environment.apiUrl}/team/all`
    )
  }

  public getById(id: number): Observable<Team> {
    return this.http.get<Team>(
      `${environment.apiUrl}/team/${id}`
    )
  }

  public getUsersByTeamId(id: number): Observable<TeamMember[]> {
    return this.http.get<TeamMember[]>(
      `${environment.apiUrl}/team/${id}/users`
    )
  }

  public create(request: CreateTeam): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/team/create`,
      request
    );
  }

  public disband(id: number): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/team/${id}/disband`,
      null
    )
  }

  public leave(id: number, userId: number): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/team/${id}/leave`,
      userId
    )
  }
}
