import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { EmployeeService } from '../service/employee.service';
import { RegisterService } from '../service/register.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css'
})
export class ProfileComponent implements OnInit {
  profileForm: FormGroup;
  profiles: any[] = [];
  selectedProfileId: string | null = null;
  states: any[] = []; // Store fetched states
  countries:any[] =[]; //store  fetched  countries

   constructor(private fb: FormBuilder, 
    private employeeService: EmployeeService, 
    private registerService: RegisterService) {

    this.profileForm = this.fb.group({
      id: [],
      dob: ['', Validators.required],
      gender: ['', Validators.required],
      workExperience: ['', Validators.required],
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
  this.getEmployeeByUserId();
   this.loadStates(); 
   this.loadCountries();
   
  
  }
// Fetch employee profile by logged-in user ID
getEmployeeByUserId() {
  const userId = this.registerService.getUserId();
  if (!userId) {
    console.error('User ID not found in session!');
    return;
  }

  this.employeeService.getEmployeeByUserId(userId).subscribe(
    (data) => {
      if (data) {
        this. profileForm.patchValue(data);
        this.selectedProfileId = data.userId;
      }
    },
    (error) => {
      console.error('Error fetching employee:', error);
    }
  );
}
loadStates() {
  this.employeeService.getstates().subscribe(data => {
    this.states = data.map(states => ({
      id: states.id,
      name: states.name
    }));
  });
}
loadCountries() {
  this.employeeService.getcountries().subscribe(data => {
    this.countries = data.map(countries => ({
      id: countries.id,
      name: countries.name
    }));
  });
}
 // Reset form and switch to Create mode
 cancel() {
  this.profileForm.reset();
  this.selectedProfileId = null;
}
 
  
  

 

  saveProfile() {
    const userId = this.registerService.getUserId();
    if (!userId) {
      alert('User ID not found!');
      return;
    }
    debugger;
    this.profileForm.get('userId')?.setValue(userId);

    if (this.profileForm.valid) {
      if (this.selectedProfileId) {
        // Update existing profile
        this.employeeService.updateEmployee(this.profileForm.value).subscribe(
          () => {
            alert('Profile updated successfully!');
            this.getEmployeeByUserId();
            this.cancel();
          },
          (error) => {
            alert('Error updating profile!');
            console.error(error);
          }
        );
      } else {
        // Create new profile
        this.employeeService.createEmployee(this.profileForm.value).subscribe(
          () => {
            alert('Profile created successfully!');
            this.getEmployeeByUserId();
            this.cancel();
          },
          (error) => {
            alert('Error creating profile!');
            console.error(error);
          }
        );
      }
    } else {
      alert('Please fill in all required fields!');
    }
  }
}
