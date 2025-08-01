import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { RegisterService } from '../../service/register.service';


@Component({
  selector: 'app-header',
  imports: [RouterModule,CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  userRole: string = '';
  isLoggedIn = false;
  constructor(private router: Router, private registerService : RegisterService,  private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.registerService.isLoggedIn$.subscribe(status => {
      this.isLoggedIn = status;
    }); 
    this.userRole = this.registerService.getrolesInfo(); 
    this.cdr.detectChanges();  // 👈 Manually trigger change detection
    setTimeout(() => {
      this.userRole = this.registerService.getrolesInfo(); 
      this.cdr.detectChanges();    }, 5000);
   }
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
