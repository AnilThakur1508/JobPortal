import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ApiServiceService } from './api-service.service';
import { environment } from '../../enviorment/enviornment';

@Injectable({
  providedIn: 'root'
})
export class JobService {
  
  
  private endpoint = 'Job';
  private apiURL = environment.baseUrl;

  constructor(private apiService: ApiServiceService) {}

  // Fetch all job listings
  getJobs(): Observable<any[]> {
    return this.apiService.get(`Job/GetAll`).pipe(
      map((response: any) => response.data || response) // Extract 'data' if exists
    );
  }
   
  // Fetch job details by ID
  getJobById(id: string): Observable<any> {
    return this.apiService.get(`Job/GetById/${id}`);
  }

  // Create a new job
  createJob(jobData: any): Observable<any> {
    return this.apiService.post(`Job/Add`, jobData);  
  }

  // Update a job
  updateJob(id: string, jobData: any): Observable<any> {
    return this.apiService.put(`Job/${id}`, jobData);  
  }

  // Delete a job
  deleteJob(id: string): Observable<any> {
    return this.apiService.delete(`Job/Delete/${id}`);
  }
  
  getcourse(): Observable<any[]> {
    return this.apiService.get<any[]>(`Course/GetAll`);
  }
  getCategories(): Observable<any[]> {
    return this.apiService.get(`Category/GetAll`);
  }
  // ✅ Fetch skills based on a selected category ID
  getSkillsByCategory(categoryId: string): Observable<any[]> {
    return this.apiService.get(`Skills/GetByCategory/${categoryId}`);
  }

}
