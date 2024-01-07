import { Component, OnInit } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { StudentsClass } from 'src/app/shared/models/students-class';
import { AdminService } from 'src/app/shared/services/admin.service';

@Component({
  selector: 'app-approve-students-dialog',
  templateUrl: './approve-students-dialog.component.html',
  styleUrls: ['./approve-students-dialog.component.scss']
})
export class ApproveStudentsDialogComponent implements OnInit {
  form = this.fb.group({
    gradeId: this.fb.control([], Validators.required)
  });
  allClasses$!: Observable<StudentsClass[]>;

  constructor(
    public dialogRef: MatDialogRef<ApproveStudentsDialogComponent>,
    private fb: NonNullableFormBuilder,
    private adminService: AdminService
  ) {}

  ngOnInit(): void {
    this.allClasses$ = this.adminService.getAllClasses();
  }

  onSubmit(): void {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      this.dialogRef.close(this.form.getRawValue());
    }
  }
}
