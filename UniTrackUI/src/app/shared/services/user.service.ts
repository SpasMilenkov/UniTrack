import { Injectable } from '@angular/core';
import { Profile } from '../models/profile';
import { StudentProfile } from '../models/student-profile';
import { ProfileTypes } from '../enums/profile-types.enum';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor() {}

  getCurrentUserProfile(): Profile | StudentProfile {
    return {
      id: '1111',
      type: ProfileTypes.STUDENT,
      avatarUrl: 'assets/images/programmer.png',
      firstName: 'John',
      lastName: 'Doe',
      number: 12345,
      classNumber: '101',
      className: 'Class A',
      grades: [
        { subject: 'Math', grade: 6 },
        { subject: 'Science', grade: 6 },
        { subject: 'History', grade: 5 },
      ],
      absences: [
        { subject: 'Math', absence: 1, excused: true },
        { subject: 'Math', absence: 1, excused: true },
        { subject: 'Science', absence: 1, excused: false },
      ],
    };
  }

  getTeacherProfile(): Profile | StudentProfile {
    return {
      type: ProfileTypes.TEACHER,
      avatarUrl: 'assets/images/teacher.png',
      firstName: 'John',
      lastName: 'Doe',
      id: '11111',
      classNumber: '101',
      className: 'Class A'
    };
  }
}
