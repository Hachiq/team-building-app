import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { TeamsComponent } from './components/teams/teams.component';
import { UsersComponent } from './components/users/users.component';
import { TeamProfileComponent } from './components/team-profile/team-profile.component';
import { RequestsComponent } from './components/requests/requests.component';
import { TeamGuard } from './guards/team.guard';

const routes: Routes = [
  { path: '',   redirectTo: 'home', pathMatch: 'full' },
  { path: "home", component: HomeComponent },
  { path: "register", component: RegisterComponent },
  { path: "login", component: LoginComponent },
  { path: "teams", component: TeamsComponent },
  { path: "users", component: UsersComponent },
  { path: "team-profile/:id", component: TeamProfileComponent },
  { path: "team-profile/:id/requests", component: RequestsComponent, canActivate:[TeamGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
