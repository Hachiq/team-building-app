import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  hide = true;

  username = new FormControl('', [Validators.required]);
  password = new FormControl('', [Validators.required]);

  constructor(private authService: AuthService, private router: Router) { }

  login() {
    this.authService.login({
      username: this.username.value,
      password: this.password.value
    }).subscribe((token: string) => {
      this.authService.setToken(token);
      this.router.navigate(['home']);
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
