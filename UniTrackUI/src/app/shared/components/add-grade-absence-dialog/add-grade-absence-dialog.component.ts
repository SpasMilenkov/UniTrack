import { Component, Inject } from '@angular/core';
import { TeacherDetailsCardTypes } from '../../enums/teacher-details-card-types.enum';
import { TeacherProfile } from '../../models/teacher-profile';
import { FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UserService } from '../../services/user.service';
import { AddGradeAbsenceDialogData } from '../../models/add-grade-absence-dialog-data';
import { Observable, filter, map, tap } from 'rxjs';
import { Subject } from '../../models/subject';
import { AdminService } from '../../services/admin.service';

@Component({
  selector: 'app-add-grade-absence-dialog',
  templateUrl: './add-grade-absence-dialog.component.html',
  styleUrls: ['./add-grade-absence-dialog.component.scss'],
})
export class AddGradeAbsenceDialogComponent {
  types = TeacherDetailsCardTypes;
  teacher!: TeacherProfile;
  subjects: Subject[] = [];

  form = this.fb.group({
    subjectId: this.fb.control('', Validators.required),
    value: this.fb.control(1, Validators.required),
    date: this.fb.control('', Validators.required),
    topic: this.fb.control(''),
  });

  constructor(
    public dialogRef: MatDialogRef<AddGradeAbsenceDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AddGradeAbsenceDialogData,
    private userService: UserService,
    private fb: FormBuilder,
    private adminService: AdminService
  ) {}

  ngOnInit(): void {
    this.teacher = this.userService.getCurrentUserProfile() as TeacherProfile;
    this.adminService
      .getAllSubjects()
      .pipe(
        tap((subjects: Subject[]) =>
          this.teacher.subjects.forEach((subName) => {
            const subjectObj = subjects.find(
              ({name}) => subName.toLowerCase() === name.toLowerCase()
            );

            if(subjectObj){
              this.subjects.push(subjectObj)
            }
          })
        )
      ).subscribe();
    if(this.data.type === this.types.ADD_GRADE){
      this.form.get('value')?.patchValue(2);
      this.form.get('value')?.setValidators(Validators.pattern(/^[2-6]$/))
    }
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
    console.log(this.form.getRawValue(), this.form.valid)
    if (this.form.valid) {
      this.dialogRef.close(this.form.getRawValue());
    }
  }
}
