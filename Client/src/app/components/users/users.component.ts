import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent {
  users!: User[];
  displayedColumns: string[] = [ 'id', 'username', 'firstName', 'lastName', 'email'];

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
      .subscribe((result: User[]) => this.users = result);
  }
}
