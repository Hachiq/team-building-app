import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Client';

  constructor (private authService: AuthService) { }

  ngOnInit() {
    this.authService.refreshToken().subscribe(() => { 
      console.log("Token refreshed successfully.") 
    }, 
    (error) => { 
      console.error("Error refreshing token", error) 
    });
  }
}
