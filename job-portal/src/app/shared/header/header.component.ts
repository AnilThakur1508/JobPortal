import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { RegisterService } from '../../service/register.service';


@Component({
  selector: 'app-header',
  imports: [RouterModule,CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  isLoggedIn = false;
  constructor(private router: Router, private registerService : RegisterService) {}

  ngOnInit(): void {
    this.registerService.isLoggedIn$.subscribe(status => {
      this.isLoggedIn = status;
    });  }
  login(): void {
    this.router.navigate(['/login']).then(() => {
      this.isLoggedIn = true;
    });
  }
  logout(): void {
    this.registerService.logout();
    window.location.href = '/login'; 
  }
}
