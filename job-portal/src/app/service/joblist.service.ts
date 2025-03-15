import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiServiceService } from './api-service.service';
import { environment } from '../../enviorment/enviornment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class joblistService {
  private endpoint = 'Job'; // Endpoint
private apiURL = environment.baseUrl
  constructor(private Http: HttpClient, private apiService: ApiServiceService) { }

  // Fetch all job listings
  getJobs(): Observable<any[]> {
    return this.apiService.get(`Job/joblist`);
  }

  // Fetch job details by ID
  getJobById(id: string): Observable<any> {
    return this.apiService.get(`Job/joblist/${id}`);
  }

  // Create a new job
  createJob(jobData: any): Observable<any> {
    return this.apiService.post(`Job/joblist`, jobData);
  }
  // Update an existing job
  updateJob(id: string, jobData: any): Observable<any> {
    return this.apiService.put(`Job/joblist/{id}`, jobData);
  }

  // Delete a job
  deleteJob(id: string): Observable<any> {
    return this.apiService.delete(`Job/joblist/${id}`);
  }
  // Fetch all categories
getCategories(): Observable<any> {
  return this.apiService.get(`Category/getAll`); // Adjust API URL as needed
}
}
 