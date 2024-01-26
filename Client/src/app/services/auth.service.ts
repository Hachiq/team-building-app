import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Register } from '../models/register';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { Login } from '../models/login';
import { tap } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private tokenSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);
  JsonWebToken$: Observable<string | null> = this.tokenSubject.asObservable();
  // private JsonWebToken?: string | null;

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

  public refreshToken(): Observable<string> {
    return this.http.get(`${environment.apiUrl}/Auth/refresh-token`, { responseType: 'text', withCredentials: true }).pipe(
      tap((response) => {
        const newToken = response;
        this.setToken(newToken);
      })
    );
  }

  public getToken(): string | null | undefined {
    return this.tokenSubject.value;
  }

  public setToken(token: string): void {
    this.tokenSubject.next(token);
  }

  public clearToken(): void {
    this.tokenSubject.next(null);
  }
}
