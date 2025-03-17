import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee',
  standalone: true,
  templateUrl: './employee-dashboard.component.html',
  styleUrl: './employee-dashboard.component.css'
})
export class EmployeeComponent {
  constructor(private router: Router) {}

  navigateToProfile() {
    this.router.navigate(['/profile']);
  }
}
