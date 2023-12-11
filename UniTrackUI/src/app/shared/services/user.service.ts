import { Injectable } from '@angular/core';
import { Profile } from '../models/profile';
import { StudentProfile } from '../models/student-profile';
import { ProfileTypes } from '../enums/profile-types.enum';
import { TeacherProfile } from '../models/teacher-profile';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor() {}

  getCurrentUserProfile(): Profile | StudentProfile {
    return {
      id: '2222',
      uniId: '1',
      type: ProfileTypes.STUDENT,
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
      ],
    };
  }

  getTeacherProfile(): TeacherProfile {
    return {
      type: ProfileTypes.TEACHER,
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
          classId: '1',
          students: [
            {
              id: '2222',
              uniId: '1',
              type: ProfileTypes.STUDENT,
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
