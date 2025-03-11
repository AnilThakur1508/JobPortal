import { Injectable } from '@angular/core';
import { environment } from '../../enviorment/enviornment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiServiceService } from './api-service.service';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private endpoint = 'Authenticate'; // Endpoint
private apiURL = environment.baseUrl
  constructor(private Http: HttpClient, private apiService: ApiServiceService) { }

  // register user
  addItem(userdata: any): Observable<any> {
    return this.apiService.post(`Authenticate/register`, userdata);
  }
  login(userdata: any): Observable<any> {
    return this.apiService.post(`Authenticate/Login`, userdata);
  }

}
