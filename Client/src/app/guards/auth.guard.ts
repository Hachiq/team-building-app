import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    tokenSubscription: Subscription | undefined;

    constructor(private authService: AuthService, private router: Router) { }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        
        // Subscribe to the token observable
        this.tokenSubscription = this.authService.JsonWebToken$.subscribe(token => {
            if (token) {
                return true;
            } else {
                this.router.navigate(['login']);
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