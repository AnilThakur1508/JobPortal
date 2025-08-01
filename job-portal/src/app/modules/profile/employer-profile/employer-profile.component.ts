import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { EmployerService } from '../../../service/employer.service';
import { RegisterService } from '../../../service/register.service';

@Component({
  selector: 'app-employer-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './employer-profile.component.html',
  styleUrls: ['./employer-profile.component.css'],
})
export class EmployerProfileComponent implements OnInit {
  profileForm: FormGroup;
  profiles: any[] = [];
  selectedProfileId: string | null = null;
  states: any[] = []; 
  countries:any[] =[]; 
  constructor(
    private fb: FormBuilder,
    private employerService: EmployerService,
    private registerService: RegisterService
  ) {
    this.profileForm = this.fb.group({
      id: [],
      companyName: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      website: ['', [Validators.required, Validators.pattern]],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      email: ['', [Validators.required, Validators.email]],
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
    this.getEmployerByUserId();
     this.loadStates(); 
     this.loadCountries();
     
    
    }

  
getEmployerByUserId() {
  const userId = this.registerService.getUserId();
  if (!userId) {
    console.error('User ID not found in session!');
    return;
  }

  this.employerService.getEmployerByUserId(userId).subscribe(
    (data) => {
      if (data) {
        this. profileForm.patchValue(data);
        this.selectedProfileId = data.userId;
      }
    },
    (error) => {
      console.error('Error fetching employer:', error);
    }
  );
}
loadStates() {
  this.employerService.getstates().subscribe(data => {
    this.states = data.map(states => ({
      id: states.id,
      name: states.name
    }));
  });
}
loadCountries() {
  this.employerService.getcountries().subscribe(data => {
    this.countries = data.map(countries => ({
      id: countries.id,
      name: countries.name
    }));
  });
}
 
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
        this.employerService.updateEmployer(this.profileForm.value).subscribe(
          () => {
            alert('Profile updated successfully!');
            this.getEmployerByUserId();
            this.cancel();
          },
          (error) => {
            alert('Error updating profile!');
            console.error(error);
          }
        );
      } else {
        this.employerService.createEmployer(this.profileForm.value).subscribe(
          () => {
            alert('Profile created successfully!');
            this.getEmployerByUserId();
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
