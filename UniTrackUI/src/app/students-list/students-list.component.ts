import { Component } from '@angular/core';
import { StudentProfile } from '../shared/models/student-profile';
import { TeacherDetailsCardTypes } from '../shared/enums/teacher-details-card-types.enum';
import { MatDialog } from '@angular/material/dialog';
import { AddGradeAbsenceDialogComponent } from '../shared/components/add-grade-absence-dialog/add-grade-absence-dialog.component';
import { TeachersService } from '../shared/services/teachers.service';

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
    private teachersService: TeachersService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.studentsData = this.teachersService.getAllStudents();
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
      if(type === this.teacherDetailsCardTypes.ADD_GRADE){
        this.teachersService.addGrade(studentId, result);
        return;
      }else if(type === this.teacherDetailsCardTypes.ADD_ABSENCE){
        this.teachersService.addAbsence(studentId, result);
        return;
      }
    });
  }
}
