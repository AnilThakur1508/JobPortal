import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterService } from '../../service/register.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  roles: any[] = [];
  selectedFile: File | null = null;

  constructor(private fb: FormBuilder, private registerService: RegisterService, private router: Router) {
    this.registerForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(3)]],
      lastName: ['', [Validators.required, Validators.minLength(3)]],
      role: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
      profilePicture: [null, Validators.required]
    });
  }

  ngOnInit(): void {
    this.getRoles();
  }

  // Fetch roles from API
  getRoles(): void {
    this.registerService.getRoles().subscribe({
      next: (response) => {
        console.log("Roles API Response:", response);
        this.roles = response;
      },
      error: (error) => console.error('Error fetching roles:', error)
    });
  }

  // Handle file selection
  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
      this.registerForm.patchValue({ profilePicture: this.selectedFile });
    }
  }

  // Submit Form
  onSubmit(): void {
    debugger;
    if (this.registerForm.valid && this.selectedFile) {
      const formData = new FormData();
      formData.append('firstName', this.registerForm.get('firstName')?.value);
      formData.append('lastName', this.registerForm.get('lastName')?.value);
      formData.append('role', this.registerForm.get('role')?.value);
      formData.append('phoneNumber', this.registerForm.get('phoneNumber')?.value);
      formData.append('email', this.registerForm.get('email')?.value);
      formData.append('password', this.registerForm.get('password')?.value);
      formData.append('confirmPassword', this.registerForm.get('confirmPassword')?.value);
      formData.append('profilePicture', this.selectedFile);

      this.registerService.addItem(formData).subscribe({
        next: (response) => {
          console.log('User registered successfully:', response);
          alert('Registration Successful!');
          this.registerForm.reset();
          this.router.navigate(['/login']);
        },
        error: (error) => {
          console.error('Error:', error);
          alert('Registration Failed!');
        }
      });
    } else {
      alert('Please fill all fields correctly');
    }
  }
}
