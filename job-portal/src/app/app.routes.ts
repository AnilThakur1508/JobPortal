import { Routes } from '@angular/router';
import { HomeComponent } from '../app/public/home/home.component';
import { AboutComponent } from './public/about/about.component';
import { ContactComponent } from './public/contact/contact.component';
import { JobListComponent } from './public/joblist/joblist.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { EmployerComponent } from './public/employer/employer.component';
import { EmployeeComponent } from './employee-dashboard/employee-dashboard.component';
import { ProfileComponent } from './profile/profile.component';

export const routes: Routes = [
{path:'',component:HomeComponent},  
{path:'home',component:HomeComponent}, 
{path:'about',component:AboutComponent},
{path:'contact',component:ContactComponent},
{path:'jobs',component:JobListComponent},
{path:'login',component:LoginComponent},
{path:'register',component:RegisterComponent},
{path:'employer',component:EmployerComponent},
{path:'employee-dashboard',component:EmployeeComponent},
{path:'profile',component:ProfileComponent}





];
