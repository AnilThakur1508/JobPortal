import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { EmployeeService } from '../service/employee.service';
import { RegisterService } from '../service/register.service';

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
  
  constructor(private fb: FormBuilder, private employeeService: EmployeeService, private registerService: RegisterService) {
    this.profileForm = this.fb.group({
      id: [''],
      dob: ['', Validators.required],
      resumeId: ['', Validators.required],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      userId: [''],
      address: this.fb.group({
        addressLine1: ['', Validators.required],
        addressLine2: [''],
        city: ['', Validators.required],
        stateId: ['', Validators.required],
        zipcode: ['', Validators.required],
        countryId: ['', Validators.required]
      })
    });
  }

  ngOnInit(): void {
  this.getProfileByUserId();
  
   
  }

  // Fetch all employee profiles
  getAllProfiles() {
    this.employeeService.getAllEmolyees().subscribe((data) => {
      this.profiles = data;
    
    });
  }
  getProfileByUserId() {
    this.employeeService.getEmployeeByUserId(this.registerService.getUserId()).subscribe((data) => {
      this.profiles = data;
   if(this.profiles!=null||this.profiles!=undefined){
    this.profileForm.patchValue(this.profiles);
    console.log("???????????",this.profiles);
    this.selectedProfileId =this.profileForm.get('userId')?.value;

   }
    });
  }
  

  // Select an employee for editing
  selectProfile(profile: any) {
    this.selectedProfileId = profile.userId;
    this.profileForm.patchValue(profile);
  }

  // Reset form and cancel editing mode
  resetForm() {
    this.profileForm.reset();
    this.selectedProfileId = null;
  }

  // Save profile (Create or Update)
  saveProfile() {
    const payload = this.registerService.getUserId();
    if (!payload) {
      alert('User ID not found!');
      return;
    }
    this.profileForm.get('userId')?.setValue(payload);

    if (this.profileForm.valid) {
      if (this.selectedProfileId) {
        // Check if employee exists before updating
        this.employeeService.getEmployeeByUserId(this.selectedProfileId).subscribe(
          (existingEmployee) => {
            if (existingEmployee) {
              // Employee exists, proceed with update
              this.employeeService.updateEmployee(this.profileForm.value).subscribe(() => {
                alert('Profile updated successfully!');
                this.getAllProfiles();
                this.resetForm();
              });
            } else {
              alert('Employee not found! Cannot update.');
            }
          },
          (error) => {
            alert('Error fetching employee data!');
            console.error(error);
          }
        );
      } else {
        // Create new profile
        this.employeeService.createEmployee(this.profileForm.value).subscribe(() => {
          alert('Profile created successfully!');
          this.getAllProfiles();
          this.resetForm();
        });
      }
    } else {
      alert('Please fill in all required fields!');
    }
  }
}
