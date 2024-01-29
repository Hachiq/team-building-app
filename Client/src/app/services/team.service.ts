import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Team } from '../models/team';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { TeamRequest } from '../interfaces/teamRequest';

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

  public create(request: TeamRequest): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/team/create`,
      request
    );
  }
}
