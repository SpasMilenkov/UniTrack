import { Injectable } from '@angular/core';
import { Profile } from '../models/profile';
import { StudentProfile } from '../models/student-profile';
import { Roles } from '../enums/roles.enum';
import { TeacherProfile } from '../models/teacher-profile';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { LocalStorageKeys } from '../enums/local-storage-keys.enum';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  getRole(): Roles {
    return localStorage.getItem(LocalStorageKeys.USER_ROLE) ? JSON.parse(localStorage.getItem(LocalStorageKeys.USER_ROLE) || '') : '';
  }

  getUserById(id: number): Observable<any>{
    return this.http.get(`http://localhost:5036/api/Student/userId/${id}`, {withCredentials: true});
  }

  getStudentById(id: number): Observable<any>{
    return this.http.get(`http://localhost:5036/api/Student/userId/${id}`, {withCredentials: true});
  }

  getTeacherById(id: number): Observable<any>{
    return this.http.get(`http://localhost:5036/api/Teacher/UserId/${id}`, {withCredentials: true});
  }

  getAdminById(id: number): Observable<any>{
    return this.http.get(`http://localhost:5036/api/Admin/GetAdminByUserId/${id}`, {withCredentials: true});
  }

  getCurrentUserProfile(): Profile | StudentProfile {
    return localStorage.getItem(LocalStorageKeys.CURRENT_USER) ? JSON.parse(localStorage.getItem(LocalStorageKeys.CURRENT_USER) || '') : {};
  }
}
