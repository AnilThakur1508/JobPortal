import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { RegisterService } from '../../service/register.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private registerService: RegisterService, private router: Router) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  ngOnInit(): void {
    const navigation = this.router.getCurrentNavigation();
    const state = navigation?.extras.state as { email: string, password: string };

    if (state) {
      this.loginForm.patchValue({
        email: state.email,
        password: state.password
      });
    }
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      this.registerService.login(this.loginForm.value).subscribe({
        next: (response) => {
          console.log('User logged in successfully:', response);
          alert('Login Successful!');
          const token = response.token;
          localStorage.setItem('authToken', token);
          this.registerService.getUserId();
          // Navigate to the desired page after successful login
          this.router.navigate(['/employee']);
           this.router.navigate(['/employer']);
        },
        error: (error) => {
          console.error('Error:', error);
          alert('Login Failed!');
        }
      });
    }
  }
}