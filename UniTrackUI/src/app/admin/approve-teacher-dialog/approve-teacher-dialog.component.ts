import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  NonNullableFormBuilder,
  Validators,
} from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { StudentsClass } from 'src/app/shared/models/students-class';
import { Subject } from 'src/app/shared/models/subject';
import { AdminService } from 'src/app/shared/services/admin.service';

@Component({
  selector: 'app-approve-teacher-dialog',
  templateUrl: './approve-teacher-dialog.component.html',
  styleUrls: ['./approve-teacher-dialog.component.scss'],
})
export class ApproveTeacherDialogComponent implements OnInit {
  form = this.fb.group({
    gradeIds: this.fb.control([], Validators.required),
    subjectIds: this.fb.control([], Validators.required),
    classId: this.fb.control(''),
  });
  allClasses!: StudentsClass[];
  allSubjects!: Subject[];

  constructor(
    public dialogRef: MatDialogRef<ApproveTeacherDialogComponent>,
    private fb: NonNullableFormBuilder,
    private adminService: AdminService
  ) {}

  ngOnInit(): void {
    this.allClasses = this.adminService.getAllClasses();
    this.allSubjects = this.adminService.getAllSubjects();
  }

  onSubmit(): void {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      this.dialogRef.close(this.form.getRawValue());
    }
  }
}
