import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiServiceService } from './api-service.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../enviorment/enviornment';

@Injectable({
  providedIn: 'root'
})
export class EmployerService {
  
  private endpoint = 'Employer';
  private apiURL = environment.baseUrl;
  constructor(private http: HttpClient, private apiService: ApiServiceService) {}
  getAllEmolyers(): Observable<any[]> {
    return this.apiService.get<any[]>(`Employer/GetAll`);
  }
  getEmployerById(employerId: string): Observable<any> {
    return this.apiService.get(`Employer/GetBy/${employerId}`);
  }
  getEmployerByUserId(Id: string): Observable<any> {
    return this.apiService.get(`Employer/GetByUserId/${Id}`);
  }
  createEmployer(employerData: FormData): Observable<any> {
    return this.apiService.post(`Employer/AddOrUpdate`,employerData);
  }
  updateEmployer( updatedData: any): Observable<any> {
    return this.apiService.post(`Employer/AddOrUpdate`, updatedData);
  }
  deleteEmployer(employerId: string): Observable<any> {
    return this.apiService.delete(`Employer/${employerId}`);
  }
   getstates(): Observable<any[]> {
    return this.apiService.get<any[]>(`states/GetAll`);
  }
  getcountries(): Observable<any[]>{
   return this.apiService.get<any[]>(`Country/GetAll`);
  }
 
}
