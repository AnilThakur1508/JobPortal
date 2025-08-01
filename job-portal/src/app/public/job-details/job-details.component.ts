import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { JobService } from '../../service/job.service';
import { RegisterService } from '../../service/register.service';

@Component({
  selector: 'app-job-details',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './job-details.component.html',
  styleUrl: './job-details.component.css'
})
export class JobDetailsComponent implements OnInit {
  job: any;
  loading = true;
  error: string | null = null;
  constructor(
    private jobService: JobService,
    private route: ActivatedRoute,
    private router: Router,
    private registerService: RegisterService
  ) {}
 ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.jobService.getJobDetail(id).subscribe({
        next: (data) => {
          this.job = data;
          this.loading = false;
        },
        error: (err) => {  
          this.loading = false;
          console.error(err);
        }
      });
    } else {
      this.error = 'No job ID provided in route.';
      this.loading = false;
    }
  }

  goBack(): void {
    this.router.navigate(['/jobs']); 
  }
  applyNow(id:string): void {
    debugger;
    const token = localStorage.getItem('authToken');
    this.router.navigate([], {
      queryParams: { id: id },
      queryParamsHandling: 'merge', 
    });
    if (token) {
      
      console.log('User is logged in. Proceed to apply.');
      alert('You have applied for this job! (You can implement actual application logic here)');
    
      this.router.navigate(['/app'], {
        queryParams: { id: id },
        queryParamsHandling: 'merge', 
      });

    } else {
     
      alert('Please login or register first to apply for jobs.');
      this.router.navigate(['/login']);
    }
  }
  
}
