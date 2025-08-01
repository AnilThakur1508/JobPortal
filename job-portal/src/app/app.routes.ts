import { Routes } from '@angular/router';
import { HomeComponent } from './public/home/home.component';
import { AboutComponent } from './public/about/about.component';
import { ContactComponent } from './public/contact/contact.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { EmployerLayoutComponent } from './layouts/employer-layout/employer-layout.component';
import { PostJobComponent } from './modules/jobs/post-job/post-job.component'; 
import { ManageJobsComponent } from './modules/jobs/manage-jobs/manage-jobs.component';
import { ViewApplicationsComponent } from './modules/jobs/view-applications/view-applications.component';
import { EmployerProfileComponent } from './modules/profile/employer-profile/employer-profile.component';
import { JobListComponent } from './public/job-list/job-list.component';
import { JobDetailsComponent } from './public/job-details/job-details.component'; 
import { ProfileComponent } from './employee/employee.component';
import { ApplyComponent } from './apply/apply.component';
export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'about', component: AboutComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'jobs', component: JobListComponent }, 
  {path: 'job/:id', component: JobDetailsComponent}, 
  { path: 'employee/profile', component: ProfileComponent }, 
  {path:'app',component:ApplyComponent},
  {
    path: 'employer',
    component: EmployerLayoutComponent, 
    children: [
     
      { path: 'post-job', component: PostJobComponent }, 
      { path: 'post-job/:id', component: PostJobComponent }, 
      { path: 'manage-jobs', component: ManageJobsComponent },
      { path: 'applications', component: ViewApplicationsComponent },
      { path: 'profile', component: EmployerProfileComponent },
    ],
  },
 { path: '**', redirectTo: '/home' },

];
