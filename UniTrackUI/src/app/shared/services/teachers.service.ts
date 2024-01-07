import { Injectable } from '@angular/core';
import { StudentProfile } from 'src/app/shared/models/student-profile';
import { students } from '../students.data';
import { Grade } from '../models/grade';
import { Absence } from '../models/absence';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root',
})
export class TeachersService {
  constructor(private http: HttpClient, private userService: UserService) {}

  getAllStudents(): Observable<StudentProfile[]> {
    const {id} = this.userService.getCurrentUserProfile();
    return this.http.get<StudentProfile[]>(`http://localhost:5036/api/Student/ByTeacherId/${id}`, {withCredentials: true});
  }

  addGrade(studentId: string, grade: Grade): Observable<any> {
    const {id} = this.userService.getCurrentUserProfile();
    console.log(grade)
    const body = {
      ...grade,
      gradedOn: grade.date,
      subjectId: +grade.subjectId,
      teacherId: +id,
      studentId: +studentId
    }
    return this.http.post(`http://localhost:5036/api/Mark`, body, {withCredentials: true});
  }

  addAbsence(studentId: string, absence: Absence) {
    const {id} = this.userService.getCurrentUserProfile();
    console.log(absence)
    const body = {
      ...absence,
      time: absence.date,
      subjectId: +absence.subjectId,
      teacherId: +id,
      studentId: +studentId
    }
    return this.http.post(`http://localhost:5036/api/Absences`, body, {withCredentials: true});
  }
}
