import { Component, Inject } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Roles } from 'src/app/shared/enums/roles.enum';

@Component({
  selector: 'app-set-user-role-dialog',
  templateUrl: './set-user-role-dialog.component.html',
  styleUrls: ['./set-user-role-dialog.component.scss'],
})
export class SetUserRoleDialogComponent {
  form = this.fb.group({
    role: this.fb.control('', Validators.required),
  });
  roles = Object.values(Roles).filter((role: string) => {
    const isAdmin = role === Roles.ADMIN;
    const isGuest = role === Roles.GUEST;
    const multipleIdsForTeacher =
      Array.isArray(this.data.ids) && role === Roles.TEACHER;

    return !isGuest && !isAdmin && !multipleIdsForTeacher;
  });

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { ids: string[] | string },
    public dialogRef: MatDialogRef<SetUserRoleDialogComponent>,
    private fb: NonNullableFormBuilder
  ) {}

  onSubmit(): void {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      this.dialogRef.close(this.form.getRawValue());
    }
  }
}
