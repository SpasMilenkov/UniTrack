import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApprovalTableComponent } from './approval-table/approval-table.component';
import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from '../shared/material.module';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
import { ApproveTeacherDialogComponent } from './approve-teacher-dialog/approve-teacher-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ApproveStudentsDialogComponent } from './approve-students-dialog/approve-students-dialog.component';
import { SetUserRoleDialogComponent } from './set-user-role-dialog/set-user-role-dialog.component';
@NgModule({
  declarations: [
    ApprovalTableComponent,
    AdminLayoutComponent,
    ApproveTeacherDialogComponent,
    ApproveStudentsDialogComponent,
    SetUserRoleDialogComponent,
  ],
  exports: [ApprovalTableComponent],
  imports: [CommonModule, SharedModule, MaterialModule, ReactiveFormsModule],
})
export class AdminModule {}
