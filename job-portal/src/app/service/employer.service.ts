import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiServiceService } from './api-service.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../enviorment/enviornment';

@Injectable({
  providedIn: 'root'
})
export class EmployerService {
  
  private endpoint = 'Employer'; // Backend API endpoint
  private apiURL = environment.baseUrl;

  constructor(private http: HttpClient, private apiService: ApiServiceService) {}

  // Fetch all employer listings
  getAllEmolyers(): Observable<any[]> {
    return this.apiService.get<any[]>(`Employer/GetAll`);
  }


  // Get a single employer by ID
  getEmployerById(employerId: string): Observable<any> {
    return this.apiService.get(`Employer/GetBy/${employerId}`);
  }
  getEmployerByUserId(Id: string): Observable<any> {
    return this.apiService.get(`Employer/GetByUserId/${Id}`);
  }

  // Create a new employer
  createEmployer(employerData: any): Observable<any> {
    return this.apiService.post(`Employer/AddOrUpdate`,employerData);
  }

  // Update an employer's details
  updateEmployer( updatedData: any): Observable<any> {
    return this.apiService.post(`Employer/AddOrUpdate`, updatedData);
  }

  // Delete an employer
  deleteEmployer(employerId: string): Observable<any> {
    return this.apiService.delete(`Employer/${employerId}`);
  }
  
  
  
 
}
