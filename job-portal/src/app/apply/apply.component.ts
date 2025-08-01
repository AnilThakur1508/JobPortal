import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ApplicationService } from '../service/application.service';
import { RegisterService } from '../service/register.service';

@Component({
  selector: 'app-apply',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './apply.component.html',
})
export class ApplyComponent implements OnInit {
  applyForm: FormGroup;
  selectedFile: File | null = null;
  jobId: string | null = null;
  EmployeeId: string | null = null;
  

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private applicationService: ApplicationService,
    private registerService: RegisterService
  ) {
    this.applyForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(3)]],
      lastName: ['', [Validators.required, Validators.minLength(3)]],
      experience: ['', [Validators.required, Validators.min(0)]],
      qualification: ['', [Validators.required, Validators.minLength(2)]],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      email: ['', [Validators.required, Validators.email]],
      appliedOn: ['', Validators.required],
      resume: [null, Validators.required],
      employeeId: [''],
      jobId: ['']
    });
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.jobId = params['id'];
      this.EmployeeId = this.registerService.getUserId();
    });
  }

  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
      this.applyForm.patchValue({ resume: this.selectedFile });
    }
  }

  onSubmit(): void {
    debugger;
    if (this.applyForm.invalid || !this.selectedFile || !this.jobId || !this.EmployeeId) {
      alert('All fields are required.');
      return;
    }

    const formData = new FormData();
    formData.append('JobId', this.jobId!);
    formData.append('EmployeeId', this.EmployeeId!);
    formData.append('FirstName', this.applyForm.get('firstName')?.value);
    formData.append('LastName', this.applyForm.get('lastName')?.value);
    formData.append('Experience', this.applyForm.get('experience')?.value);
    formData.append('Email', this.applyForm.get('email')?.value);
    formData.append('PhoneNumber', this.applyForm.get('phoneNumber')?.value);
    formData.append('Qualification', this.applyForm.get('qualification')?.value);
    formData.append('AppliedOn', this.applyForm.get('appliedOn')?.value);
    formData.append('Resume', this.selectedFile!);

    console.log('Submitting application with FormData:');
    formData.forEach((value, key) => console.log(key, value));

    this.applicationService.applyForJob(formData).subscribe({
      next: (res) => {
        console.log('Success response:', res);
        alert('Job application submitted successfully.');
        this.router.navigate(['/employer/view-applications']);
        this.applyForm.reset();
        
      },
      error: (err) => {
        console.error('Submission error:', err);
        alert('Submission failed.');
      }
    });
  }

  cancel(): void {
    this.applyForm.reset();
    
  }
}
