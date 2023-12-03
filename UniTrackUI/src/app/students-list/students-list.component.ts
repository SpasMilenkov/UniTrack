import { Component } from '@angular/core';
import { StudentsService } from '../shared/services/students.service';
import { StudentProfile } from '../shared/models/student-profile';

@Component({
  selector: 'app-students-list',
  templateUrl: './students-list.component.html',
  styleUrls: ['./students-list.component.scss']
})
export class StudentsListComponent {
  studentsData: StudentProfile[] = [];
  displayedColumns: string[] = ['avatarUrl', 'firstName', 'lastName', 'className', 'number', 'actions'];

  constructor(private studentsService: StudentsService) { }

  ngOnInit(): void {
    this.studentsData = this.studentsService.getAllStudents();
  }
}
