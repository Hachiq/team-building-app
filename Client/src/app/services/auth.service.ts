import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Register } from '../models/register';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { Login } from '../models/login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  public register(user: Register): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/Auth/register`,
      user
    );
  }

  public login(user: Login): Observable<string> {
    return this.http.post(`${environment.apiUrl}/Auth/login`, user, {
      responseType: 'text',
    });
  }
}
