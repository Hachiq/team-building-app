import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { DisplayUser } from 'src/app/models/displayUser';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent {
  users?: DisplayUser[];
  tokenSubscription: Subscription | undefined;
  
  constructor (private userService: UserService, private authService: AuthService) { }

  ngOnInit(): void {
    this.tokenSubscription = this.authService.JsonWebToken$.subscribe(token => {
      if (token) {
        this.loadUsers();
      }
    });
  }

  ngOnDestroy(): void {
    if (this.tokenSubscription) {
      this.tokenSubscription.unsubscribe();
    }
  }

  loadUsers() {
    this.userService
      .get()
      .subscribe((result: DisplayUser[]) => this.users = result);
  }
}
