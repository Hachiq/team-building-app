import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Stats } from '../models/stats';
import { TeamStats } from '../models/teamStats';

@Injectable({
  providedIn: 'root'
})
export class StatsService {

  constructor(private http: HttpClient) { }

  public getByUserId(id: number) : Observable<Stats> {
    return this.http.get<Stats>(
      `${environment.apiUrl}/stats/user?id=${id}`
    )
  }

  public getByTeamId(id: number) : Observable<TeamStats> {
    return this.http.get<TeamStats>(
      `${environment.apiUrl}/stats/team?id=${id}`
    )
  }

  public update(id?: number, salary?: number | null) : Observable<any> {
    return this.http.put<any>(
      `${environment.apiUrl}/stats/${id}/salary`,
      salary
    )
  }

  public addDayWorked(id: number): Observable<any> {
    return this.http.put<any>(
      `${environment.apiUrl}/stats/${id}/add-day-worked`,
      null
    )
  }

  public addDayPaid(id: number): Observable<any> {
    return this.http.put<any>(
      `${environment.apiUrl}/stats/${id}/add-day-paid`,
      null
    )
  }
}
