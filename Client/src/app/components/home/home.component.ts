import { Component } from '@angular/core';
import { DisplayUser } from 'src/app/models/displayUser';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  users?: DisplayUser[];
  
  constructor (private userService: UserService) {
    this.loadUsers();
   }

  loadUsers(){
    this.userService
      .get()
      .subscribe((result: DisplayUser[]) => this.users = result);
  }
}
