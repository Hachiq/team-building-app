import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Team } from '../models/team';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { TeamRequest } from '../interfaces/teamRequest';
import { User } from '../models/user';

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

  public getUsersByTeamId(id: number): Observable<User[]> {
    return this.http.get<User[]>(
      `${environment.apiUrl}/team/${id}/users`
    )
  }

  public create(request: TeamRequest): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/team/create`,
      request
    );
  }
}
