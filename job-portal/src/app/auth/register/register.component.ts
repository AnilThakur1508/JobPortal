import { Component, OnInit } from '@angular/core';
import { RegisterService } from '../../service/register.service';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';




@Component({

  selector: 'app-register',
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],  
})
export class RegisterComponent {
  registerForm: FormGroup;
roles: any[]=[{id:1,name:'Admin'},{id:2,name:'User'},
  {id:3,name:'SuperAdmin'}
];
  constructor(private fb: FormBuilder,private registerService:RegisterService) {

    this.registerForm= this.fb.group({
     
      firstName: ['', [Validators.required, Validators.minLength(3)]], // First Name validation
      lastName: ['', [Validators.required, Validators.minLength(3)]], // Last Name validation
      role: ['', [Validators.required]], 
      phoneNumber: ['', [Validators.required,Validators.pattern('^[0-9]{10}$')]], // Phone Number validation  
      email: ['', [Validators.required, Validators.email]], // Email validation
      password: ['', [Validators.required, Validators.minLength(6)]] , // Password validation
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]] , // Confirm Password validation
     profilePicture: ['', [Validators.required]] // Profile Picture validation
    });
     
  }
   onSubmit(): void {
    debugger;
alert('Form Submitted');
        if (this.registerForm.valid) {
          console.log(this.registerForm.value);
        }
        this.registerService.addItem(this.registerForm.value).subscribe({
          next: (response) => { console.log('user added:', response); } ,
          error: (error) => console.error("Error", error) });
  }   
}
