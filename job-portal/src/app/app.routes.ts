import { Routes } from '@angular/router';

// Public Pages
import { HomeComponent } from './public/home/home.component';
import { AboutComponent } from './public/about/about.component';
import { ContactComponent } from './public/contact/contact.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';

// Employer Layout & Pages
import { EmployerLayoutComponent } from './layouts/employer-layout/employer-layout.component';
import { EmployerDashboardComponent } from './modules/dashboard/employer-dashboard/employer-dashboard.component';
import { PostJobComponent } from './modules/jobs/post-job/post-job.component'; // ✅ Missing Import Fixed
import { ManageJobsComponent } from './modules/jobs/manage-jobs/manage-jobs.component';
import { ViewApplicationsComponent } from './modules/jobs/view-applications/view-applications.component';
import { EmployerProfileComponent } from './modules/profile/employer-profile/employer-profile.component';


export const routes: Routes = [
  // Public Pages
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'about', component: AboutComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  // Employer Layout with Child Routes
  {
    path: 'employer',
    component: EmployerLayoutComponent, // This wraps all employer pages with a layout
    children: [
      { path: 'dashboard', component: EmployerDashboardComponent },
      { path: 'post-job', component: PostJobComponent }, // ✅ Add Job
      { path: 'post-job/:id', component: PostJobComponent }, // ✅ Edit Job
      { path: 'manage-jobs', component: ManageJobsComponent },
      { path: 'applications', component: ViewApplicationsComponent },
      { path: 'profile', component: EmployerProfileComponent },
    ],
  },
 

  // Redirect unknown routes
  { path: '**', redirectTo: '/home' },
];
