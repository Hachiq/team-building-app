import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Stats } from 'src/app/models/stats';
import { User } from 'src/app/models/user';
import { StatsService } from 'src/app/services/stats.service';
import { TokenService } from 'src/app/services/token.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.scss'
})
export class UserProfileComponent {
  userId!: number;

  user!: User;
  stats!: Stats;

  statAsDataSource: Stats[] = [];
  displayedColumns: string[] = [ 'daysWorked', 'daysPaid', 'salary'];

  salary = new FormControl();

  constructor(private route: ActivatedRoute, private userService: UserService, private statsService: StatsService, private tokenService: TokenService) {
    route.params.subscribe(params => {
      this.userId = +params['id']; // Convert to number
    });
  }

  firstName = new FormControl('', [Validators.maxLength(50)]);
  lastName = new FormControl('', [Validators.maxLength(50)]);

  ngOnInit() {
    this.loadUser();
    this.loadStats();
  }

  loadUser() {
    this.userService.getById(this.userId)
      .subscribe((result) => this.user = result);
  }

  loadStats() {
    this.statsService.getByUserId(this.userId)
      .subscribe((result) => {
        this.statAsDataSource = [],
        this.stats = result,
        this.statAsDataSource.push(this.stats);    
      }
    );
  }

  canChangeProfile(): boolean {
    return this.tokenService.getUserIdFromToken() == this.user.id;
  }

  canChangeSalary() : boolean {
    return this.tokenService.userIsLeader();
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
    });
  }

  updateSalary(){
    this.statsService.update(this.stats.id, this.salary.value).subscribe(() => {   
      this.loadStats();
    }, (error) => {
      if (error.status === 400) {
        console.log("Something wrong");
      }
    });
  }

  getTotalReceiving(){
    return this.stats.daysPaid * this.stats.salary;
  }

  getCompanyDebt(){
    return (this.stats.daysWorked - this.stats.daysPaid) * this.stats.salary;
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
