import { Injectable } from '@angular/core';
import { CreateRequest } from '../interfaces/createRequest';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Request } from '../models/request';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  constructor(private http: HttpClient) { }

  public get(id: number): Observable<Request[]> {
    return this.http.get<Request[]>(
      `${environment.apiUrl}/team/${id}/requests`
    )
  }

  public create(request: CreateRequest): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/request/create`,
      request
    );
  }

  public accept(id: number): Observable<any> {
    return this.http.put<any>(
      `${environment.apiUrl}/request/${id}/accept`,
      null
    )
  }

  public decline(id: number): Observable<any> {
    return this.http.put<any>(
      `${environment.apiUrl}/request/${id}/decline`,
      null
    )
  }
}
