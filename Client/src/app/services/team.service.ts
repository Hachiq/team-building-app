import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DisplayTeam } from '../models/displayTeam';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

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
}
