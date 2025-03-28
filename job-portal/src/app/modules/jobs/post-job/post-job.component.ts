import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {  FormBuilder,  FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgMultiSelectDropDownModule, IDropdownSettings } from 'ng-multiselect-dropdown';
import { RegisterService } from '../../../service/register.service';
import { JobService } from '../../../service/job.service';

@Component({
  selector: 'app-post-job',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgMultiSelectDropDownModule],
  templateUrl: './post-job.component.html',
  styleUrls: ['./post-job.component.css']

})
export class PostJobComponent implements OnInit { // ✅ Class name fixed
  jobForm: FormGroup;
  selectedJobId: string | null = null;
  experienceOptions: string[] = ['Fresher', '1', '2', '3', '4', '5+'];
  courses: any[] = []; // List of courses
  selectedCourses: any[] = []; // Stores selected courses
  dropdownSettings: IDropdownSettings = {}; // Dropdown settings

  constructor(
    private fb: FormBuilder,
    private jobService: JobService,
    private registerService: RegisterService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.jobForm = this.fb.group({
      Id: [''],
      Title: ['', [Validators.required, Validators.minLength(3)]],
      Description: ['', [Validators.required, Validators.minLength(3)]],
      Experience: [''],
      EmployerId:[''],
      courseIds: [[], Validators.required], 
      jobType: ['', [Validators.required]],
      Salary: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      PublishDate: ['', [Validators.required]],
      ExpiryDate: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    debugger;
    this.loadCourses();
    // Configure Multi-Select Dropdown Settings
    this.dropdownSettings = {
      singleSelection: false, // ✅ Allow multiple selections
      idField: 'id', // ✅ Unique ID of the course
      textField: 'name', // ✅ Name of the course
        selectAllText: 'Select All',
        unSelectAllText: 'Unselect All',
        itemsShowLimit: 3, // Show up to 3 selected courses
      allowSearchFilter: true, // Enable search functionality
    };


    



    this.route.paramMap.subscribe((params) => {
      this.selectedJobId = params.get('id');
       debugger;
      if (this.selectedJobId) {
        this.jobService.getJobById(this.selectedJobId).subscribe({
          next: (job) => {
            this.jobForm.patchValue({
              Id: job.JobId,
              Title: job.title,
              Description: job.description,
              jobType: job.jobType,
              EmployerId:job.employerId,
              courseIds: job.courseIds || [],
              Salary: job.salary,
              Experience: job.experience,
              PublishDate: job.postedDate ? job.postedDate.split('T')[0] : '',
              ExpiryDate: job.expiryDate ? job.expiryDate.split('T')[0] : ''
            });
            // Set selected courses in dropdown
            this.selectedCourses = this.courses.filter(course => job.courseIds?.includes(course.id));
           
          },

          error: (error) => {
            console.error('Error fetching job:', error);
            alert('Error fetching job details.');
          }
        });
      }
    });
  }
  loadCourses() {
    this.jobService.getcourse().subscribe(data => {
      this.courses = data.map(course => ({
        id: course.id,
        name: course.name
      }));
    });
  }
 
  onCourseSelect(item: any) {
    debugger;
    console.log('Selected Course:', item);
    const selectedIds = this.jobForm.get('courseIds')?.value || [];
    selectedIds.push(item.id);
    this.jobForm.patchValue({ courseIds: selectedIds });
  }
  
  onCourseDeselect(item: any) {
    debugger;
    console.log('Deselected Course:', item);
    let selectedIds = this.jobForm.get('courseIds')?.value || [];
    selectedIds = selectedIds.filter((id: any) => id !== item.id);
    this.jobForm.patchValue({ courseIds: selectedIds });
  }
  
  onSelectAll(items: any) {
    debugger;
    console.log('All Selected:', items);
    this.jobForm.patchValue({ courseIds: items.map((item: any) => item.id) });
  }
  
  onDeSelectAll() {
    debugger;
    console.log('All Deselected');
    this.jobForm.patchValue({ courseIds: [] });
  }
  
  onSubmit(): void {
    debugger;
    if (this.jobForm.valid) {
      const jobData = this.jobForm.value;
      jobData.Id = this.selectedJobId;
      
      if (this.selectedJobId) {
        this.jobService.updateJob(this.selectedJobId, jobData).subscribe({
          next: () => {
            alert('Job Updated Successfully!');
            this.router.navigate(['/employer/manage-jobs']); // ✅ Fixed Path
          },
          error: (error) => {
            console.error('Error updating job:', error);
            alert('Error updating job. Please try again.');
          }
        });
      } else {
      jobData.EmployerId = this.registerService.getUserId(); // Ensure EmployeeId is always set
        this.jobService.createJob(jobData).subscribe({
          next: () => {
            alert('Job Created Successfully!');
            this.router.navigate(['/employer/manage-jobs']); // ✅ Fixed Path
          },
          error: (error) => {
            console.error('Error adding job:', error);
            alert('Error adding job. Please try again.');
          }
        });
      }
    } else {
      alert('Form is invalid. Please check the fields.');
    }
  }

   cancel(): void {
     this.router.navigate(['/employer/manage-jobs']); // ✅ Fixed Path
   }
 }







 

