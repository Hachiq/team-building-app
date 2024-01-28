import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor(private authService: AuthService) { }

  public getUserIdFromToken(): number {
    const token = this.authService.getToken();
    if (token) {
      const decodedToken = jwtDecode<any>(token);
      return parseInt(decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'], 10);
    }
    return 0;
  }
}
