import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { TeamService } from 'src/app/services/team.service';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-team-form',
  templateUrl: './team-form.component.html',
  styleUrl: './team-form.component.scss'
})
export class TeamFormComponent {
  name = new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]);

  constructor (private teamService: TeamService, private tokenService: TokenService) { }

  create(){
    this.teamService.create({
      userId: this.tokenService.getUserIdFromToken(),
      name: this.name.value
    }).subscribe(() => {},
      (error) => {
        if (error.status === 409){
          const reason = error.error.reason;
          if (reason === 'NameTaken') {
            this.name.setErrors({ namingConflict: true });
            
          }
          else if (reason === "UserAlreadyInTeam") {
            this.name.setErrors({ userAlreadyInTeam: true });
          }
        }
        else {
          console.log('Undefined error. Please, try again later.');
        }
      }
    )
  }

  getNameErrorMessage() {
    if (this.name.hasError('required')) {
      return 'You must enter a value';
    }

    if (this.name.hasError('namingConflict')) {
      return 'Name is taken';
    }

    if (this.name.hasError('userAlreadyInTeam')) {
      return 'You already have a team';
    }

    if (this.name.hasError('minlength')){
      return 'Name too short';
    }

    if (this.name.hasError('maxlength')){
      return 'Name too long';
    }
    
    return '';
  }
}
