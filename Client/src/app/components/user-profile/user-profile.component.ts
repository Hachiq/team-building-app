import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.scss'
})
export class UserProfileComponent {
  userId!: number;

  user?: User;

  constructor(private route: ActivatedRoute, private userService: UserService) {
    route.params.subscribe(params => {
      this.userId = +params['id']; // Convert to number
    });
  }

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {
    this.userService.getById(this.userId)
      .subscribe((result) => this.user = result);
  }
}
