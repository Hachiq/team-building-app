import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Stats } from '../models/stats';

@Injectable({
  providedIn: 'root'
})
export class StatsService {

  constructor(private http: HttpClient) { }

  public getById(id: number) : Observable<Stats> {
    return this.http.get<Stats>(
      `${environment.apiUrl}/stats/${id}`
    )
  }

  public update(id?: number, salary?: number | null) : Observable<any> {
    return this.http.put<any>(
      `${environment.apiUrl}/stats/${id}/salary`,
      salary
    )
  }
}
