import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentsListComponent } from './students-list.component';
import { AppRoutingModule } from '../app-routing.module';
import { MaterialModule } from '../shared/material.module';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [StudentsListComponent],
  exports: [StudentsListComponent],
  imports: [CommonModule, AppRoutingModule, MaterialModule, SharedModule],
})
export class StudentsListModule {}
