import { Component, Inject } from '@angular/core';
import { TeacherDetailsCardTypes } from '../../enums/teacher-details-card-types.enum';
import { TeacherProfile } from '../../models/teacher-profile';
import { FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UserService } from '../../services/user.service';
import { AddGradeAbsenceDialogData } from '../../models/add-grade-absence-dialog-data';

@Component({
  selector: 'app-add-grade-absence-dialog',
  templateUrl: './add-grade-absence-dialog.component.html',
  styleUrls: ['./add-grade-absence-dialog.component.scss'],
})
export class AddGradeAbsenceDialogComponent {
  types = TeacherDetailsCardTypes;
  teacher!: TeacherProfile;
  form = this.fb.group({
    subject: this.fb.control('', Validators.required),
    grade: this.fb.control(2, Validators.pattern(/^[2-6]$/)),
    date: this.fb.control(''),
    topic: this.fb.control(''),
  });

  constructor(
    public dialogRef: MatDialogRef<AddGradeAbsenceDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AddGradeAbsenceDialogData,
    private userService: UserService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.teacher = this.userService.getTeacherProfile();
  }

  getTypeTitle(): string {
    let typeTitle = '';
    switch (this.data.type) {
      case this.types.ADD_GRADE:
        typeTitle = 'grade';
        break;
      case this.types.ADD_ABSENCE:
        typeTitle = 'absence';
        break;
      case this.types.ADD_EVENT:
        typeTitle = 'event';
        break;
    }

    return typeTitle;
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.dialogRef.close();
    }
  }
}
