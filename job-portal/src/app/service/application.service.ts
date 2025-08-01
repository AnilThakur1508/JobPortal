import { Injectable } from '@angular/core';
import { ApiServiceService } from './api-service.service';
import { environment } from '../../enviorment/enviornment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {
  private endpoint = 'JobApplication';
  private apiURL = environment.baseUrl;
  constructor(private apiService: ApiServiceService) { }

 applyForJob(userdata: FormData): Observable<any> {
 return this.apiService.post(`JobApplication/apply`,userdata,{isFormData:true});
 }
  getAllApplications(): Observable<any> {
    return this.apiService.get(`JobApplication/all`);
  }
   GetStatuses(): Observable<any> {
    return this.apiService.get(`appStatus/statuses`);
  }
  updateStatus(Id: any, StatusId: number): Observable<any> {
    return this.apiService.put(`${this.endpoint}/${Id}/Status?appStatus=${StatusId}`,StatusId); 
  }
  getById(Id: string): Observable<any> {
    return this.apiService.get(`JobApplication/${Id}`);
  }
}

