import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  
  constructor(private authService: AuthService, private router: Router) { }
  
  hide = true;

  username = new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]);
  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required, Validators.minLength(6)]);

  register(){
    this.authService.register({
      username: this.username.value,
      email: this.email.value,
      password: this.password.value
    }).subscribe(() => {
      this.router.navigate(['login']);
    },
      (error) => {
        if (error.status === 409){
          this.username.setErrors({ conflict: true });
        }
        else {
          console.log('Undefined error. Please, try again later.');
        }
      }
    );
  }

  getUsernameErrorMessage() {
    if (this.username.hasError('required')) {
      return 'You must enter a value';
    }

    if (this.username.hasError('conflict')) {
      return 'Username is taken';
    }

    if (this.username.hasError('minlength')){
      return 'Name too short';
    }

    if (this.username.hasError('maxlength')){
      return 'Name too long';
    }
    
    return '';
  }

  getEmailErrorMessage() {
    if (this.email.hasError('required')) {
      return 'You must enter a value';
    }

    return this.email.hasError('email') ? 'Not a valid email' : '';
  }

  getPasswordErrorMessage() {
    if (this.password.hasError('required')) {
      return 'You must enter a value';
    }

    return this.password.hasError('minlength') ? 'Password too short' : '';
  }

}
