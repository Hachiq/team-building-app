import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DisplayUser } from '../models/displayUser';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  public get() : Observable<DisplayUser[]>{
    return this.http.get<DisplayUser[]>(
      `${environment.apiUrl}/user/all`
    )
  }
}
