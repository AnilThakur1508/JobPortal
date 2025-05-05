import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { JobService } from '../../../service/job.service';
 

@Component({
  selector: 'app-manage-jobs',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './manage-jobs.component.html',
  styleUrls: ['./manage-jobs.component.css']
})
export class ManageJobsComponent implements OnInit {
  jobs: any[] = [];
 constructor(private jobService: JobService, private router: Router) {} 
  ngOnInit(): void {
    this.getAllJobs();
    
  }
  getAllJobs(): void {
    this.jobService.getJobs().subscribe({
      next: (response) => {
        this.jobs = Array.isArray(response) ? response : (response as any).data;
      },
      error: (error) => console.error('Error fetching jobs:', error)
    });
  }
  addJob(): void {
    this.router.navigate(['/employer/post-job']); 
  }
  editJob(jobId: string): void {
    this.router.navigate(['/employer/post-job', jobId]); 
  }
  deleteJob(id: string): void {
    debugger;
    if (confirm('Are you sure you want to delete this job?')) {
      this.jobService.deleteJob(id).subscribe({
        next: () => {
          alert('Job deleted successfully.');
          this.getAllJobs();
        },
        error: (error) => {
          console.error('Error deleting job:', error);
          alert('Error deleting job. Please try again.');
        }
      });
    } 
  }
 
}
