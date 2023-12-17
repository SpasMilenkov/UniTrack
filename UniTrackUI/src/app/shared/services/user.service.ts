import { Injectable } from '@angular/core';
import { Profile } from '../models/profile';
import { StudentProfile } from '../models/student-profile';
import { Roles } from '../enums/roles.enum';
import { TeacherProfile } from '../models/teacher-profile';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  getRole(): Roles {
    return Roles.ADMIN;
  }

  getUserById(id: number): Observable<any>{
    return this.http.get(`http://localhost:5036/api/Student/${id}`, {withCredentials: true})
  }

  getCurrentUserProfile(): Profile | StudentProfile {
    return {
      id: '2222',
      uniId: '1',
      type: Roles.STUDENT,
      avatarUrl: 'assets/images/programmer.png',
      firstName: 'John',
      lastName: 'Doe',
      number: 12345,
      classId: '101',
      className: 'Class A',
      classTeacherFirstName: 'Teacher1',
      classTeacherLastName: 'LastName1',
      grades: [
        {
          subject: 'Math',
          grade: 6,
          date: '2023-12-01T12:00:00',
          teacherId: '11111',
          teacherFirstName: 'John',
          teacherLastName: 'Doe',
          topic: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor.'
        },
        {
          subject: 'Science',
          grade: 6,
          date: '2023-11-21T12:00:00',
          teacherId: '11111',
          teacherFirstName: 'John',
          teacherLastName: 'Doe',
          topic: 'Dui ut ornare lectus sit. Vel quam elementum pulvinar etiam non quam lacus suspendisse faucibus.'
        },
        {
          subject: 'History',
          grade: 5,
          date: '2023-12-01T08:00:00',
          teacherId: '11111',
          teacherFirstName: 'John',
          teacherLastName: 'Doe',
          topic: ' Nisl nunc mi ipsum faucibus vitae aliquet nec ullamcorper sit.'
        },
      ],
      absences: [
        {
          subject: 'Math',
          absence: 1,
          excused: true,
          date: '2023-12-02T13:00:00',
          teacherId: '11111',
          teacherFirstName: 'John',
          teacherLastName: 'Doe',
        },
        {
          subject: 'Math',
          absence: 1,
          excused: true,
          date: '2023-12-01T13:00:00',
          teacherId: '11111',
          teacherFirstName: 'John',
          teacherLastName: 'Doe',
        },
        {
          subject: 'Science',
          absence: 1,
          excused: false,
          date: '2023-12-01T12:00:00',
          teacherId: '11111',
          teacherFirstName: 'John',
          teacherLastName: 'Doe',
        },
      ],
    };
  }

  getTeacherProfile(): TeacherProfile {
    return {
      type: Roles.TEACHER,
      uniId: '1',
      avatarUrl: 'assets/images/teacher.png',
      firstName: 'John',
      lastName: 'Doe',
      id: '11111',
      classId: '101',
      className: 'Class A',
      subjects: ['Math', 'History', 'Music', 'Science'],
      classes: [
        {
          classId: 1,
          className: 'Class A',
          students: [
            {
              id: '2222',
              uniId: '1',
              type: Roles.STUDENT,
              avatarUrl: 'assets/images/programmer.png',
              firstName: 'John',
              lastName: 'Doe',
              number: 12345,
              classId: '101',
              className: 'Class A',
              classTeacherFirstName: 'Teacher1',
              classTeacherLastName: 'LastName1',
              grades: [
                {
                  subject: 'Math',
                  grade: 6,
                  date: '2023-12-01T12:00:00',
                  teacherId: '11111',
                  teacherFirstName: 'John',
                  teacherLastName: 'Doe',
                },
                {
                  subject: 'Science',
                  grade: 6,
                  date: '2023-11-21T12:00:00',
                  teacherId: '11111',
                  teacherFirstName: 'John',
                  teacherLastName: 'Doe',
                },
                {
                  subject: 'History',
                  grade: 5,
                  date: '2023-12-01T08:00:00',
                  teacherId: '11111',
                  teacherFirstName: 'John',
                  teacherLastName: 'Doe',
                },
              ],
              absences: [
                {
                  subject: 'Math',
                  absence: 1,
                  excused: true,
                  date: '2023-12-02T13:00:00',
                  teacherId: '11111',
                  teacherFirstName: 'John',
                  teacherLastName: 'Doe',
                },
                {
                  subject: 'Math',
                  absence: 1,
                  excused: true,
                  date: '2023-12-01T13:00:00',
                  teacherId: '11111',
                  teacherFirstName: 'John',
                  teacherLastName: 'Doe',
                },
                {
                  subject: 'Science',
                  absence: 1,
                  excused: false,
                  date: '2023-12-01T12:00:00',
                  teacherId: '11111',
                  teacherFirstName: 'John',
                  teacherLastName: 'Doe',
                },
              ]
            }
          ],
          classTeacher: {} as TeacherProfile,
        }
      ],
    } as TeacherProfile;
  }
}
