import { Injectable } from '@angular/core';
import { StudentProfile } from 'src/app/shared/models/student-profile';
import { students } from '../students.data';
import { Grade } from '../models/grade';
import { Absence } from '../models/absence';

@Injectable({
  providedIn: 'root',
})
export class TeachersService {
  constructor() {}

  getAllStudents(): StudentProfile[] {
    return students;
  }

  addGrade(studentId: string, grade: Grade) {
    console.log(studentId, grade);
  }

  addAbsence(studentId: string, absence: Absence) {
    console.log(studentId, absence);
  }
}
