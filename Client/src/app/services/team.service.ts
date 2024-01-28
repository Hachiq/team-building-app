import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DisplayTeam } from '../models/displayTeam';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { TeamRequest } from '../models/teamRequest';

@Injectable({
  providedIn: 'root'
})
export class TeamService {

  constructor(private http: HttpClient) { }

  public get(): Observable<DisplayTeam[]> {
    return this.http.get<DisplayTeam[]>(
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
