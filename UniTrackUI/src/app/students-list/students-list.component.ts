import { Component } from '@angular/core';
import { StudentsService } from '../shared/services/students.service';
import { StudentProfile } from '../shared/models/student-profile';
import { TeacherDetailsCardTypes } from '../shared/enums/teacher-details-card-types.enum';
import { MatDialog } from '@angular/material/dialog';
import { AddGradeAbsenceDialogComponent } from '../shared/components/add-grade-absence-dialog/add-grade-absence-dialog.component';

@Component({
  selector: 'app-students-list',
  templateUrl: './students-list.component.html',
  styleUrls: ['./students-list.component.scss'],
})
export class StudentsListComponent {
  studentsData: StudentProfile[] = [];
  teacherDetailsCardTypes = TeacherDetailsCardTypes;

  displayedColumns: string[] = [
    'firstName',
    'lastName',
    'className',
    'number',
    'actions',
  ];

  constructor(
    private studentsService: StudentsService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.studentsData = this.studentsService.getAllStudents();
  }

  openDialog(
    studentId: string,
    studentFirstName: string,
    studentLastName: string,
    type: TeacherDetailsCardTypes
  ): void {
    const dialogRef = this.dialog.open(AddGradeAbsenceDialogComponent, {
      data: { type, studentFirstName, studentLastName, studentId },
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
    });
  }
}
