import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Component } from '@angular/core';
import { JobService } from '../../service/job.service';
import { RouterModule } from '@angular/router'; 

@Component({
  selector: 'app-job-list',
  standalone: true,
  imports: [CommonModule, FormsModule,RouterModule],
  

  templateUrl: './job-list.component.html',
  styleUrl: './job-list.component.css'
})
export class JobListComponent {
  JobFeatured: any[] = [];
  TotalCount: number = 0;
  PageNumber: number = 1;
  PageSize: number = 5;
  categories: any[] = [];
  selectedCategory: string = '';
  jobTypes: any[] = [];
  selectedJobTypes: string = '';
  experienceLevels: any[] = [];
  selectedExperienceLevel: string = '';
  selectedSort = '';
 constructor(private jobService: JobService) {}

  ngOnInit(): void {
    
    this.getAllFeaturedJobs();
    this.loadCategories();
    this.loadJobTypes();
    this.loadExperienceLevels();
   
  }
  loadCategories() {
    this.jobService.getCategories().subscribe(data => {
      this.categories = data.map(category => ({
        id: category.id,
        name: category.name
      }));
    });
  }
  loadJobTypes() {
    this.jobService.getJobTypes().subscribe(data => {
      this.jobTypes = data.map(JobType => ({
        id: JobType.id,
        name: JobType.name
      }));
    });
  }
  loadExperienceLevels() {
    this.jobService.getExperienceLevels().subscribe(data => {
      this.experienceLevels = data.map(experienceLevel => ({
        id: experienceLevel.id,
        name: experienceLevel.name
      }));
    });
  }
  getAllFeaturedJobs(): void {
    const filters: any = {};
    if (this.selectedCategory) {
      filters.categoryId = this.selectedCategory;
    }
    if (this.selectedExperienceLevel) {
      filters.experienceLevelId = this.selectedExperienceLevel;
    }
    if (this.selectedJobTypes) {
      filters.jobTypeId = this.selectedJobTypes;
    }
    if (this.selectedSort) {
      filters.sort = this.selectedSort;
    }
    this.jobService.getJobsfeatured(filters, this.PageNumber, this.PageSize).subscribe({
      next: (res: any) => {
        this.JobFeatured = res.jobs || [];
        this.TotalCount = res.totalCount || 0;
        console.log("Jobs Loaded:", this.JobFeatured);
      },
      error: (err: any) => {
        console.error('Error fetching jobs:', err);
      }
    });
  }
   getTotalPages(): number {
      return Math.ceil(this.TotalCount / this.PageSize);
    }
  
    onPageChange(newPage: number): void {
      if (newPage < 1 || newPage > this.getTotalPages()) return;
      this.PageNumber = newPage;
      this.getAllFeaturedJobs();
      
    }
    
    onFilterChange(): void {
     
      this.PageNumber = 1;
      this.getAllFeaturedJobs();
    }
    onCategoryChange(): void {
      this.PageNumber = 1;
      this.getAllFeaturedJobs();
    }
    onJobTypeChange(): void {
      this.PageNumber = 1;
      this.getAllFeaturedJobs();
    }
    onExperienceLevelChange(): void {
      this.PageNumber = 1;
      this.getAllFeaturedJobs();
    }
    onSortChange(): void {
      this.PageNumber = 1;
      this.getAllFeaturedJobs();
    }
    
  }
