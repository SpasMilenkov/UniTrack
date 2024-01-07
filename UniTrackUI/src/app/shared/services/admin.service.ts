import { Injectable } from '@angular/core';
import { UserRequest } from '../models/user-request';
import { Roles } from '../enums/roles.enum';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { StudentsClass } from '../models/students-class';
import { Subject } from '../models/subject';
import { ApproveTeacherData } from '../models/approve-teacher-data';
import { School } from '../models/school';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  constructor(private http: HttpClient, private userService: UserService) {}

  approveStudents(usersData: any): Observable<any> {
    console.log(usersData);
    return this.http.put<any>('http://localhost:5036/api/Approval/students', usersData, {withCredentials: true});
  }

  approveTeacher(teacherData: ApproveTeacherData) {
    console.log(teacherData);
  }

  disApproveUsers(userIds: string) {
    console.log(userIds);
  }

  getAllSubjects(): Observable<Subject[]> {
    return this.http.get<Subject[]>('http://localhost:5036/api/Subjects', {withCredentials: true})
  }

  getAllSchools(): Observable<School[]> {
    // return this.http.get<School[]>('http://localhost:5036/api/Schools', {withCredentials: true})
    return of([
      { name: 'School 1', id: '1' },
      { name: 'School 2', id: '2' },
    ]);
  }

  getAllClasses(): StudentsClass[] {
    // return this.http.post('http://localhost:5036/api/', {withCredentials: true})
    return [
      { classId: 1, className: 'Class A', students: [] },
      { classId: 2, className: 'Class B', students: [] },
      { classId: 3, className: 'Class C', students: [] },
      { classId: 4, className: 'Class D', students: [] },
    ];
  }

  getUserApprovalRequests(): Observable<UserRequest[]> {
    const {schoolId} = this.userService.getCurrentUserProfile();

    return this.http.get<UserRequest[]>('http://localhost:5036/api/Admin/GetAllGuests/' + schoolId, {withCredentials: true})
  }
}
