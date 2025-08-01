import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiServiceService } from './api-service.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../enviorment/enviornment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private endpoint = 'Employee'; 
  private apiURL = environment.baseUrl;
  constructor(private http: HttpClient, private apiService: ApiServiceService) {}
  getAllEmolyees(): Observable<any[]> {
    return this.apiService.get<any[]>(`Employee/GetAll`);
  }
  getEmployeeById(employeeId: string): Observable<any> {
    return this.apiService.get(`Employee/GetBy/${employeeId}`);
  }
  getEmployeeByUserId(Id: string): Observable<any> {
    return this.apiService.get(`Employee/GetByUserId/${Id}`);
  }
  createEmployee(employeeData: any): Observable<any> {
    return this.apiService.post(`Employee/AddOrUpdate`,employeeData);
  }
  updateEmployee( updatedData: any): Observable<any> {
    return this.apiService.post(`Employee/AddOrUpdate`, updatedData);
  }
  deleteEmployee(employeeId: string): Observable<any> {
    return this.apiService.delete(`Employee/${employeeId}`);
  }
  getstates(): Observable<any[]> {
    return this.apiService.get<any[]>(`states/GetAll`);
  }
  getcountries(): Observable<any[]>{
   return this.apiService.get<any[]>(`Country/GetAll`);
  }
}
