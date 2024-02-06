import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { TokenService } from '../services/token.service';
import { AuthService } from '../services/auth.service';

@Injectable({
    providedIn: 'root'
})
export class TeamGuard implements CanActivate {
    tokenSubscription: Subscription | undefined;

    constructor(private authService: AuthService, private tokenService: TokenService, private router: Router) { }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        
        // Subscribe to the token observable
        this.tokenSubscription = this.authService.JsonWebToken$.subscribe(token => {
          if (token) {
            // Token is loaded, proceed with checking team ID
            const teamIdFromToken = this.tokenService.getTeamIdFromToken();
            const teamIdFromRoute = +next.paramMap.get('id')!;
    
            if (teamIdFromToken === teamIdFromRoute) {
              return true; // Allow navigation
            } else {
              this.router.navigate(['login']); // Navigate away if team ID doesn't match
              return false;
            }
          } else {
            // Token is not yet loaded, do nothing or handle accordingly
            // You may choose to redirect to a login page or show a loading indicator
            return false;
          }
        });
        
        return true; // This is temporary, canActivate will return false until the token is loaded
    }

    ngOnDestroy() {
        if(this.tokenSubscription){
            this.tokenSubscription.unsubscribe();
        }
    }
}