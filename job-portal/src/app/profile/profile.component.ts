import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { EmployeeService } from '../service/employee.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  profileForm: FormGroup;
  profiles: any[] = [];
  selectedProfileId: string | null = null;

  constructor(private fb: FormBuilder, private employeeService: EmployeeService) {
    this.profileForm = this.fb.group({
     
      dob: ['', Validators.required],
      resumeId: ['', Validators.required],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      
      address: this.fb.group({
        addressLine1: ['', Validators.required],
        addressLine2: ['', Validators.required],
        city: ['', Validators.required],
        state: ['', Validators.required],
        zipCode: ['', Validators.required],
        country: ['', Validators.required]
      })
    });
  }

  ngOnInit(): void {
    this.getAllProfiles();
  }

  // Get all profiles
  getAllProfiles() {
    this.employeeService.getAllEmolyees().subscribe((data) => {
      this.profiles = data;
    });
  }

  // Select a profile for updating
  selectProfile(profile: any) {
    this.selectedProfileId = profile.userId;
    this.profileForm.patchValue(profile);
  }

  // Create or update profile
  saveProfile() {
    if (this.profileForm.valid) {
      if (this.selectedProfileId) {
        // Update existing profile
        this.employeeService.updateEmployee(this.selectedProfileId, this.profileForm.value).subscribe(() => {
          alert('Profile updated successfully!');
          this.getAllProfiles();
          this.profileForm.reset();
          this.selectedProfileId = null;
        });
      } else {
        // Create new profile
        this.employeeService.createEmployee(this.profileForm.value).subscribe(() => {
          alert('Profile created successfully!');
          this.getAllProfiles();
          this.profileForm.reset();
        });
      }
    } else {
      alert('Please fill in all required fields!');
    }
  }
}
