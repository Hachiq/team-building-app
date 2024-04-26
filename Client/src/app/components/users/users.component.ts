import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})

export class UsersComponent {
  displayedColumns: string[] = [ 'index', 'username', 'firstName', 'lastName', 'email', 'isEmployed'];
  dataSource!: MatTableDataSource<User>;

  constructor(private userService: UserService){
    this.loadUsers();
  }

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  loadUsers() {
    this.userService
      .get()
      .subscribe((result: User[]) => { 
        this.dataSource = new MatTableDataSource<User>(result);
        this.dataSource.paginator = this.paginator;
      } 
    );
  }
}
