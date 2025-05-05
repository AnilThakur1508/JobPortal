import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder,  FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
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
export class PostJobComponent implements OnInit { 
  jobForm: FormGroup;
  selectedJobId: string | null = null
  courses: any[] = [];
  categories: any[] = [];
  selectedCategory: string = '';
  jobTypes: any[] = []; 
  selectedJobType: string = ''; 
  experienceLevels: any[] = []; 
  selectedExperienceLevel: string = ''; 
  skills: any[] = [];
  selectedCourses: any[] = []; 
  selectedSkills: any[] = [];
  dropdownSettings: IDropdownSettings = {}; 

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
      ExperienceLevelId: ['',Validators.required],
      EmployerId:[''],
      CategoryId: ['', Validators.required], 
      SkillIds: [[], Validators.required], 
      courseIds: [[], Validators.required], 
      JobTypeId: ['', [Validators.required]],
      Salary: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      PublishDate: ['', [Validators.required]],
      ExpiryDate: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    debugger;
    this.loadCourses();
    this.loadJobTypes(); 
    this.loadExperienceLevels();
    this.loadCategories(); 
    this.dropdownSettings = {
        singleSelection: false, 
        idField: 'id', 
        textField: 'name', 
        selectAllText: 'Select All',
        unSelectAllText: 'Unselect All',
        itemsShowLimit: 3, 
      allowSearchFilter: true, 
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
         
        this.jobService.getJobById(this.selectedJobId).subscribe({
          next: (job) => {
            
            debugger;
            this.jobForm.patchValue({
              Id: job.JobId,
              Title: job.title,
              Description: job.description,
              JobTypeId: job.jobTypeId,
              EmployerId:job.employerId,
              Salary: job.salary,
              ExperienceLevelId: job.experienceLevelId,
              PublishDate: job. publishDate? job.publishDate.split('T')[0] : '',
              ExpiryDate: job.expiryDate ? job.expiryDate.split('T')[0] : '',
              CategoryId: job.categoryId
            });
            this.selectedCategory = job.categoryId;
            this.loadSkills(this.selectedCategory);
            this.jobForm.patchValue({ CategoryId: job.categoryId });
           this.selectedSkills = this.skills.filter(skill => job.skillIds?.includes(skill.id));
           this.jobForm.patchValue({ SkillIds: this.selectedSkills.map(skill => skill.id) });
           this.selectedCourses = this.courses.filter(course => job.courseIds?.includes(course.id));
           this.jobForm.patchValue({ courseIds: this.selectedCourses.map(course => course.id) }); 
           this.jobForm.patchValue({ courseIds: this.selectedCourses.map(course => course.id) });
           this.jobForm.patchValue({ SkillIds: this.selectedSkills.map(skill => skill.id) });
           this.jobForm.patchValue({ CategoryId: job.categoryId });
          
          
          },

          error: (error) => {
            console.error('Error fetching job:', error);
            alert('Error fetching job details.');
          }
        });
      }
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
    this.jobForm.patchValue({ courseIds: this.selectedCourses.map(course => course.id) });
  }
  
  onCourseDeselect(item: any) {
    console.log('Deselected Course:', item);
    this.selectedCourses = this.selectedCourses.filter(course => course.id !== item.id);
    this.jobForm.patchValue({ courseIds: this.selectedCourses.map(course => course.id) });
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
    this.jobForm.patchValue({ SkillIds: this.selectedSkills.map(skill => skill.id) });
  }

  onSkillDeselect(item: any) {
    this.selectedSkills = this.selectedSkills.filter(skill => skill.id !== item.id);
    this.jobForm.patchValue({ SkillIds: this.selectedSkills.map(skill => skill.id) });
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







