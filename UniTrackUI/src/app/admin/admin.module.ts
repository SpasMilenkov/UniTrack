import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApprovalTableComponent } from './approval-table/approval-table.component';
import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from '../shared/material.module';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
@NgModule({
  declarations: [
    ApprovalTableComponent,
    AdminLayoutComponent,
  ],
  exports: [ApprovalTableComponent],
  imports: [CommonModule, SharedModule, MaterialModule],
})
export class AdminModule {}
