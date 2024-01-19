import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Login } from 'src/app/models/login';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  user: Login = {
    username: '',
    password: ''
  }

  hide = true;

  username = new FormControl('', [Validators.required]);
  password = new FormControl('', [Validators.required]);

  constructor(private authService: AuthService) { }

  login() {
    this.authService.login(this.user).subscribe((token: string) => {
      localStorage.setItem('authToken', token);
    },
      (error) => {
        if(error.status === 400){
          this.username.setErrors({ nomatch: true });
          console.log(error.error);
        }
        else {
          console.log("Undefined error. Please, try again later")
        }
      }
    );
  }

  getUsernameErrorMessage() {
    return this.username.hasError('nomatch') ? 'User not found or wrong password' : '';
  }
}
