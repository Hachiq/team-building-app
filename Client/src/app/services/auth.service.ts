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

  private JsonWebToken?: string | null;

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

  public getToken(): string | null | undefined {
    return this.JsonWebToken;
  }

  public setToken(token: string): void {
    this.JsonWebToken = token;
    console.log(`U are logged in. Token: ${this.JsonWebToken}`);
  }

  public clearToken(): void {
    this.JsonWebToken = null;
    console.log(`U are logged out. Token: ${this.JsonWebToken}`);
  }
}
