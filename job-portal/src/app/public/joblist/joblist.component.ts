import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { joblistService } from '../../service/joblist.service';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-joblist',
  imports: [CommonModule,ReactiveFormsModule],

  templateUrl: './joblist.component.html',
  styleUrls: ['./joblist.component.css']
  
})
export class JobListComponent implements OnInit {
  JobForm: FormGroup;
  jobs: any[] = []; // Store job list
  categories: any[] = []; // Store categories from API
  selectedJobId: string | null = null; // Store selected job for update
  experienceOptions: string[] = ['Fresher', '1', '2', '3', '4', '5+'];

   

  constructor(private fb: FormBuilder, private joblistService: joblistService) {
    this.JobForm = this.fb.group({
      jobTitle: ['', [Validators.required, Validators.minLength(3)]],
      jobDescription: ['', [Validators.required, Validators.minLength(3)]],
      jobExperience: ['', [Validators.required, Validators.pattern('^[0-9]*$')]], // Experience in years
      jobQualification: ['', [Validators.required]],
      jobType: ['', [Validators.required]],
      jobCategory: ['', [Validators.required]], // Job Category
      
      jobSalary: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      
      
      jobPostedDate: ['', [Validators.required]],
      jobExpiryDate: ['', [Validators.required]],
      
    });
  }
  

  ngOnInit(): void {
    this.getAllJobs();
    this.getCategories(); // Fetch categories on component load
  }
  

  // Get all jobs
  getAllJobs(): void {
    this.joblistService.getJobs().subscribe({
      next: (response) => {
        this.jobs = response;
        console.log('Jobs loaded:', this.jobs);
      },
      error: (error) => console.error('Error fetching jobs:', error)
    });
  }
  // Fetch job categories from API
  getCategories(): void {
    this.joblistService.getCategories().subscribe({
      next: (response) => {
        this.categories = response;
        console.log('Categories loaded:', this.categories); // Debugging
      },
      error: (error) => console.error('Error fetching categories:', error)
    });
  }
  
  // Create a new job
  onSubmit(): void {
    if (this.JobForm.valid) {
      if (this.selectedJobId) {
        // If updating an existing job
        this.joblistService.updateJob(this.selectedJobId, this.JobForm.value).subscribe({
          next: (response) => {
            console.log('Job updated:', response);
            alert('Job Updated Successfully!');
            this.getAllJobs(); // Refresh the list
            this.resetForm();
          },
          error: (error) => console.error('Error updating job:', error)
        });
      } else {
        // Creating a new job
        this.joblistService.createJob(this.JobForm.value).subscribe({
          next: (response) => {
            console.log('Job added:', response);
            alert('Job Created Successfully!');
            this.getAllJobs(); // Refresh the list
            this.resetForm();
          },
          error: (error) => console.error('Error adding job:', error)
        });
      }
    } else {
      alert('Form is invalid');
    }
  }

  // Set job data for update
  editJob(job: any): void {
    this.selectedJobId = job.id;
    this.JobForm.patchValue(job);
  }
  

  // Delete job
  deleteJob(id: string): void {
    if (confirm('Are you sure you want to delete this job?')) {
      this.joblistService.deleteJob(id).subscribe({
        next: () => {
          console.log('Job deleted:', id);
          alert('Job Deleted Successfully!');
          this.getAllJobs(); // Refresh the list
        },
        error: (error) => console.error('Error deleting job:', error)
      });
    }
  }

  // Reset form
  resetForm(): void {
    this.JobForm.reset();
    this.selectedJobId = null;
  }
}
