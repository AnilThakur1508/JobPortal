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
  getJobs(): Observable<any[]> {
    return this.apiService.get(`Job/GetAll`).pipe(
      map((response: any) => response.data || response) 
    );
  }
   
  
  getJobById(id: string): Observable<any> {
    return this.apiService.get(`Job/GetById/${id}`);
  }

 
  createJob(jobData: any): Observable<any> {
    return this.apiService.post(`Job/Add`, jobData);  
  }

  
  updateJob(id: string, jobData: any): Observable<any> {
    return this.apiService.put(`Job/${id}`, jobData);  
  }

  
  deleteJob(id: string): Observable<any> {
    return this.apiService.delete(`Job/Delete/${id}`);
  }
  
  getcourse(): Observable<any[]> {
    return this.apiService.get<any[]>(`Course/GetAll`);
  }
  getCategories(): Observable<any[]> {
    return this.apiService.get(`Category/GetAll`);
  }
  
  getSkillsByCategory(categoryId: string): Observable<any[]> {
    return this.apiService.get(`Skills/GetByCategory/${categoryId}`);
  }
  getJobCountsByCategory(): Observable<any[]> {
    return this.apiService.get(`Job/job-counts-by-category`);
  }
  getJobFeatured(): Observable<any[]> {
    return this.apiService.get(`Job/Featured`);
  }
  getJobsfeatured(filters: any = {}, pageNumber: number = 1, pageSize: number = 5): Observable<any> {
    const url = `Job/JobFeaturedFilter?pageNumber=${pageNumber}&pageSize=${pageSize}`;
    return this.apiService.post(url, filters);  
  }
  getJobTypes(): Observable<any[]> {
    return this.apiService.get(`JobType/GetAll`);
  }
  getExperienceLevels(): Observable<any[]> {
    return this.apiService.get(`ExperienceLevel/GetAll`);
  }
  getJobDetail(id: string): Observable<any> {
    return this.apiService.get(`Job/details/${id}`);
  }
  searchJobsByTitle(keyword: string): Observable<any[]> {
    return this.apiService.get<[]>(`Job/search?keyword=${encodeURIComponent(keyword)}`);
  }
}
