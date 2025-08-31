import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { AuthenticationResponse } from '../models/authentication-response';
import { Register } from '../models/register';
import { environment } from '../../environment';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  private baseUrl: string = environment.apiUrl;
  public isAuthenticated: boolean = false;
  public isAdmin: boolean = false;
  public currentUserName: string | null = "";

  constructor(private http: HttpClient) {
    // Check local storage for authentication status on application startup
    this.isAuthenticated = !!localStorage.getItem('authToken');
    const isAdminValue = localStorage.getItem('isAdmin');
    this.isAdmin = isAdminValue !== null && isAdminValue !== undefined && isAdminValue.toLowerCase() === 'true';
    this.currentUserName = localStorage.getItem("currentUserName");
  }

  register(register: Register): Observable<AuthenticationResponse> {
    return this.http.post<AuthenticationResponse>(`${this.baseUrl}register`, register);
  }

  login(email: string, password: string): Observable<AuthenticationResponse> {
    // Check if the provided email and password match the admin user
    if (email === 'admin@example.com' && password === 'admin') {
      // If it's the admin user, return a custom Observable
      const adminUser: AuthenticationResponse = {
        userID: 'admin_id',
        personName: 'Admin',
        email: 'admin@example.com',
        gender: 'male', // Add the appropriate gender for the admin user
        token: 'admin_token',
        success: true
      };

      return of(adminUser);
    } else {
      return this.http.post<AuthenticationResponse>(`${this.baseUrl}login`, { email, password });
    }
  }

  setAuthStatus(token: string, isAdmin: boolean, currentUserName: string): void {
    this.isAuthenticated = true;
    this.isAdmin = isAdmin;
    localStorage.setItem('authToken', token);
    localStorage.setItem('isAdmin', isAdmin.toString());
    localStorage.setItem('currentUserName', currentUserName);
    this.currentUserName = currentUserName;
  }

  logout(): void {
    this.isAuthenticated = false;
    localStorage.removeItem('authToken');
    localStorage.removeItem('isAdmin');
    localStorage.removeItem('currentUserName');
  }
}
