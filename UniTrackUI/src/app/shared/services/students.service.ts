import { Injectable } from '@angular/core';
import { StudentProfile } from 'src/app/shared/models/student-profile';
import { students } from '../students.data';

@Injectable({
  providedIn: 'root',
})
export class StudentsService {
  constructor() {}

  getAllStudents(): StudentProfile[] {
    return students;
  }
}
