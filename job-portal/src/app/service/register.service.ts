import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { ApiServiceService } from './api-service.service';
import { environment } from '../../enviorment/enviornment';
import { JwtPayload, jwtDecode } from 'jwt-decode';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
 jwtHelper: JwtHelperService;
  decodedToken: any;
  private endpoint = 'Authenticate';
  private apiURL = environment.baseUrl;
  currentUser: any;
  private loggedIn = new BehaviorSubject<boolean>(this.hasToken());
  get isLoggedIn$() {
    return this.loggedIn.asObservable();
  }
  private hasToken(): boolean {
    if (typeof window !== 'undefined') {
      return !!localStorage.getItem('authToken');
    }
    return false;
  }
  constructor(private http: HttpClient, private apiService: ApiServiceService) {
    this.jwtHelper = new JwtHelperService();
  }
  addItem(userdata: FormData): Observable<any> {
    return this.apiService.post(`Authenticate/upload-register`, userdata, { isFormData: true });
  }
  getrolesInfo(){
     this.decodedToken = this.jwtHelper.decodeToken(localStorage.getItem('authToken')||"") || null;
     return this.decodedToken?.role; 
  }
  getRoles(): Observable<any> {
    return this.apiService.get(`roles/all`);
  }
  login(userdata: any): Observable<any> {
    
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(`${this.apiURL}/Authenticate/Login`, userdata, { headers }).pipe(
      map((response: any) => {
        const token = response.token;
        localStorage.setItem('authToken', token);
        this.decodedToken = jwtDecode<JwtPayload>(token);
        return response;
      }
    )); 
  }
  getUserId(): any {
    debugger;
    const token = localStorage.getItem('authToken');
    if (token) {
      const payload = jwtDecode<JwtPayload>(token);
      return payload.sub;
    } else {
    
      console.error('Token is null');
    }
  }
  setLoginStatus(): void {
    this.loggedIn.next(true);
  }

  logout(): void {
    localStorage.removeItem('authToken');
    this.loggedIn.next(false);
  }

}