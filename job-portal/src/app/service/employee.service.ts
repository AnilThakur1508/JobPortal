import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiServiceService } from './api-service.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../enviorment/enviornment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  
  private endpoint = 'Employee'; // Backend API endpoint
  private apiURL = environment.baseUrl;

  constructor(private http: HttpClient, private apiService: ApiServiceService) {}

  // Fetch all employee listings
  getAllEmolyees(): Observable<any[]> {
    return this.apiService.get<any[]>(`Employee/GetAll`);
  }


  // Get a single employee by ID
  getEmployeeById(employeeId: string): Observable<any> {
    return this.apiService.get(`Employee/GetBy/${employeeId}`);
  }
  getEmployeeByUserId(Id: string): Observable<any> {
    return this.apiService.get(`Employee/GetByUserId/${Id}`);
  }

  // Create a new employee
  createEmployee(employeeData: any): Observable<any> {
    return this.apiService.post(`Employee/AddOrUpdate`,employeeData);
  }

  // Update an employee's details
  updateEmployee( updatedData: any): Observable<any> {
    return this.apiService.post(`Employee/AddOrUpdate`, updatedData);
  }

  // Delete an employee
  deleteEmployee(employeeId: string): Observable<any> {
    return this.apiService.delete(`Employee/${employeeId}`);
  }
  // GetAll 
}
