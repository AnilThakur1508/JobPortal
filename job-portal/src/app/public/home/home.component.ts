import { Component, OnInit } from '@angular/core';
import { JobService } from '../../service/job.service';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  categoryCounts: any[] = [];
  JobFeatured: any[] = [];
  searchKeyword: string = '';
jobTypeId: any;

  constructor(private jobService: JobService,private router: Router) {}

  ngOnInit(): void {
    this.loadJobCategoryCounts();
    this.loadJobFeatured();
  }

  loadJobCategoryCounts(): void {
    this.jobService.getJobCountsByCategory().subscribe({
      next: (data) => {
        this.categoryCounts = data;
      },
      error: (error) => {
        console.error('Error fetching job category counts:', error);
      }
    });
    
  }
  loadJobFeatured(): void {
    debugger
    this.jobService.getJobFeatured().subscribe({
      next: (data) => {
        this.JobFeatured = data;
      },
      error: (error) => {
        console.error('Error fetching featured jobs:', error);
      }
    });
  }
  searchJobs(): void {
    if (this.searchKeyword.trim()) {
      this.router.navigate(['/jobs'], { queryParams: { search: this.searchKeyword.trim() } });
    } else {
      this.router.navigate(['/jobs']);
    }
  
} 
}
