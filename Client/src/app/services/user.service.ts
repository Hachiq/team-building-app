import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';
import { UserCredentials } from '../interfaces/user.credentials';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  public get() : Observable<User[]>{
    return this.http.get<User[]>(
      `${environment.apiUrl}/user/all`
    )
  }

  public getById(id: number) : Observable<User> {
    return this.http.get<User>(
      `${environment.apiUrl}/user/${id}`
    )
  }

  public update(id?: number, creds?: UserCredentials) : Observable<any> {
    return this.http.put<any>(
      `${environment.apiUrl}/user/${id}/update`,
      creds
    )
  }
}
