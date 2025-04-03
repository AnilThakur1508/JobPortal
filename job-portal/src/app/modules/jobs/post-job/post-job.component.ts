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
  categories: any[] = [];
  selectedCategory: string = '';
  skills: any[] = [];
  
  selectedCourses: any[] = []; // Stores selected courses
  selectedSkills: any[] = [];
  dropdownSettings: IDropdownSettings = {}; // Dropdown settings

  constructor(
    private fb: FormBuilder,
    private jobService: JobService,
    private registerService: RegisterService,
    private route: ActivatedRoute,
    private router: Router,
    
  ) {
    this.jobForm = this.fb.group({
      Id: [''],
      Title: ['', [Validators.required, Validators.minLength(3)]],
      Description: ['', [Validators.required, Validators.minLength(3)]],
      Experience: [''],
      EmployerId:[''],
      CategoryId: ['', Validators.required], // ✅ Added CategoryId field
      SkillIds: [[], Validators.required], // ✅ Added SkillIds field
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
    this.loadCategories(); // ✅ Load categories first
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
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Select All',
      unSelectAllText: 'Unselect All',
      itemsShowLimit: 3,
      allowSearchFilter: true,
    }; 

    



    this.route.paramMap.subscribe((params) => {
      this.selectedJobId = params.get('id');
       debugger;
      if (this.selectedJobId) {
         // ✅ Update case - Fetch job details
        this.jobService.getJobById(this.selectedJobId).subscribe({
          next: (job) => {
            this.jobForm.patchValue({
              Id: job.JobId,
              Title: job.title,
              Description: job.description,
              jobType: job.jobType,
              EmployerId:job.employerId,
              Salary: job.salary,
              Experience: job.experience,
              
              PublishDate: job. publishDate? job.publishDate.split('T')[0] : '',
              ExpiryDate: job.expiryDate ? job.expiryDate.split('T')[0] : '',
              CategoryId: job.categoryId
            });
            this.selectedCategory = job.categoryId;
            this.loadSkills(this.selectedCategory);
            this.jobForm.patchValue({ CategoryId: job.categoryId });
            
            this.selectedSkills = this.skills.filter(skill => job.skillIds?.includes(skill.id));
            this.jobForm.patchValue({ SkillIds: this.selectedSkills.map(skill => skill.id) });
            // ✅ Set selected courses correctly
          this.selectedCourses = this.courses.filter(course => job.courseIds?.includes(course.id));
          this.jobForm.patchValue({ courseIds: this.selectedCourses.map(course => course.id) }); 
           
           // ✅ Update the form field
           this.jobForm.patchValue({ courseIds: this.selectedCourses.map(course => course.id) });
           
          
    
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
  loadCategories() {
    this.jobService.getCategories().subscribe(data => {
      this.categories = data.map(category => ({
        id: category.id,
        name: category.name
      }));
    });
  }

  loadSkills(categoryId: string) {
    this.jobService.getSkillsByCategory(categoryId).subscribe(data => {
      this.skills = data.map(skill => ({
        id: skill.id,
        name: skill.name
      }));
    });
  }
  
  onCategoryChange(event: any) {
    this.selectedCategory = event.target.value;
    this.selectedSkills = [];  // ✅ Clear selected skills
    this.jobForm.patchValue({ SkillIds: [] }); // ✅ Reset SkillIds in form
    this.loadSkills(this.selectedCategory);
  }
 
  
  onCourseSelect(item: any) {
    console.log('Selected Course:', item);
    this.selectedCourses.push(item);
    this.jobForm.patchValue({ courseIds: this.selectedCourses.map(course => course.name) });
  }
  
  onCourseDeselect(item: any) {
    console.log('Deselected Course:', item);
    this.selectedCourses = this.selectedCourses.filter(course => course.id !== item.id);
    this.jobForm.patchValue({ courseIds: this.selectedCourses.map(course => course.name) });
  }
  
  onSelectAll(items: any) {
    console.log('All Selected:', items);
    this.selectedCourses = items;
    this.jobForm.patchValue({ courseIds: items.map((item: { id: any; }) => item.id) });
  }
  
  onDeSelectAll() {
    console.log('All Deselected');
    this.selectedCourses = [];
    this.jobForm.patchValue({ courseIds: [] });
  }
   // Skill Selection
   onSkillSelect(item: any) {
    this.selectedSkills.push(item);
    this.jobForm.patchValue({ SkillIds: this.selectedSkills.map(skill => skill.name) });
  }

  onSkillDeselect(item: any) {
    this.selectedSkills = this.selectedSkills.filter(skill => skill.id !== item.id);
    this.jobForm.patchValue({ SkillIds: this.selectedSkills.map(skill => skill.name) });
  }

  onSelectAllSkills(items: any) {
    this.selectedSkills = items;
    this.jobForm.patchValue({ SkillIds: items.map((item: { id: any; }) => item.id) });
  }

  onDeSelectAllSkills() {
    this.selectedSkills = [];
    this.jobForm.patchValue({ SkillIds: [] });
  }
  
  onSubmit(): void {
    debugger;
    if (this.jobForm.valid) {
      const jobData = this.jobForm.value;
      jobData.Id = this.selectedJobId;
    // // ✅ Ensure `courseIds` is properly formatted
    // jobData.courseIds = this.selectedCourses.length 
    //   ? this.selectedCourses.map(course => course.id).join(',') 
    //   : '';
      // ✅ Format `courseIds` and `skillIds` as comma-separated strings
      jobData.courseIds = this.selectedCourses.length 
        ? this.selectedCourses.map(course => course.id).join(',') 
        : '';
        jobData.SkillIds = this.selectedSkills.length 
        ? this.selectedSkills.map(skill => skill.id).join(',') 
        : '';
      
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







