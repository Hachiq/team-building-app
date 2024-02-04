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

  public getUsernameFromToken(): string {
    const token = this.authService.getToken();
    if (token) {
      const decodedToken = jwtDecode<any>(token);
      return decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    }
    return '';
  }

  public getTeamIdFromToken(): number {
    const token = this.authService.getToken();
    if (token) {
      const decodedToken = jwtDecode<any>(token);
      return parseInt(decodedToken['TeamId'], 10);
    }
    return 0;
  }

  public userIsLeader(): boolean{
    const token: any = this.authService.getToken();
    try {
      const decodedToken: any = jwtDecode(token);
      const userRoles: string[] = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
  
      return userRoles && userRoles.includes("Leader");
    } catch (error) {
      return false; // Return false on error (invalid token, decoding error, etc.)
    }
  }
}
