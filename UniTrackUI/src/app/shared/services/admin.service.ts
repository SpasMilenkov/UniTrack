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
    console.log(usersData)
    return this.http.put<any>('http://localhost:5036/api/Approval/students', usersData, {withCredentials: true});
  }

  approveTeacher(teacherData: ApproveTeacherData) {
    const body = {
      classId: +teacherData.classId,
      schoolId: +teacherData.schoolId,
      gradeIds: teacherData.gradeIds?.map(grade => +grade),
      subjectIds: teacherData.subjectIds.map(subject => +subject)
    }
    return this.http.put<any>('http://localhost:5036/api/Approval/teachers', body, {withCredentials: true});
  }

  getAllSubjects(): Observable<Subject[]> {
    return this.http.get<Subject[]>('http://localhost:5036/api/Subjects', {withCredentials: true})
  }

  getAllSchools(): Observable<School[]> {
    return this.http.get<School[]>('http://localhost:5036/api/School', {withCredentials: true})
  }

  getAllClasses(): Observable<StudentsClass[]> {
    const {schoolId} = this.userService.getCurrentUserProfile();
    return this.http.get<StudentsClass[]>(`http://localhost:5036/api/Grade/getSubjectsBySchoolId/${schoolId}`, {withCredentials: true})
  }

  getUserApprovalRequests(): Observable<UserRequest[]> {
    const {schoolId} = this.userService.getCurrentUserProfile();

    return this.http.get<UserRequest[]>('http://localhost:5036/api/Admin/GetAllUsers/' + schoolId, {withCredentials: true})
  }
}
