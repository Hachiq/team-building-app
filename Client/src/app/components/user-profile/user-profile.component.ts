import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
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

  firstName = new FormControl('', [Validators.maxLength(50)]);
  lastName = new FormControl('', [Validators.maxLength(50)]);

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {
    this.userService.getById(this.userId)
      .subscribe((result) => this.user = result);
  }

  save() {
    this.userService.update(
      this.user?.id,
      { 
        firstName: this.firstName.value, 
        lastName: this.lastName.value 
      }
    ).subscribe(() => {
      this.loadUser();
    }, (error) => {
      if (error.status === 400) {
        console.log("Something wrong");
      }
    })
  }

  getFirstNameErrorMessage() {
    if (this.firstName.hasError('maxlength')){
      return 'Name too long';
    }

    return '';
  }

  getLastNameErrorMessage() {
    if (this.lastName.hasError('maxlength')){
      return 'Name too long'
    }

    return '';
  }
}
