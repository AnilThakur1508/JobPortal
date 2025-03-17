import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Component } from '@angular/core';
import { RegisterService } from '../../service/register.service';

@Component({
  selector: 'app-login',
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;
  constructor(private fb: FormBuilder,private registerService:RegisterService,) {

    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]], // Email validation
      password: ['', [Validators.required, Validators.minLength(6)]]  // Password validation
    });
     
  }
  onSubmit(): void {
alert('Form Submitted');
    if (this.loginForm.valid) {
      console.log(this.loginForm.value);
    }
    this.registerService.login(this.loginForm.value).subscribe({
      next: (response) => { console.log('user added:', response); } ,
      error: (error) => console.error("Error", error) });
  }
}
