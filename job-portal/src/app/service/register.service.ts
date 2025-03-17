import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiServiceService } from './api-service.service';
import { environment } from '../../enviorment/enviornment';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private endpoint = 'Authenticate';
  private apiURL = environment.baseUrl;

  constructor(private http: HttpClient, private apiService: ApiServiceService) {}

  // Register user (Uses FormData for file upload)
  addItem(userdata: FormData): Observable<any> {
    return this.apiService.post(`Authenticate/upload-register`, userdata, { isFormData: true });
  }

  // Fetch only "Employee" and "Employer" roles
  getRoles(): Observable<any> {
    return this.apiService.get(`roles/all`);
  }
   // Login user (Use JSON, NOT FormData)
   login(userdata: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(`${this.apiURL}/Authenticate/Login`, userdata, { headers });
  }
}
