import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './profile/profile.component';
import { ProfileDetailsComponent } from './profile-details/profile-details.component';
import { StudentDetailsCardComponent } from './student-details-card/student-details-card.component';
import { TeacherDetailsCardComponent } from './teacher-details-card/teacher-details-card.component';
import { MaterialModule } from '../shared/material.module';
import { SharedModule } from '../shared/shared.module';
import { AppRoutingModule } from '../app-routing.module';
import { GradesTableComponent } from './grades-table/grades-table.component';
import { StudentsListModule } from '../students-list/students-list.module';



@NgModule({
  declarations: [
    ProfileComponent,
    ProfileDetailsComponent,
    StudentDetailsCardComponent,
    TeacherDetailsCardComponent,
    GradesTableComponent
  ],
  exports: [
    ProfileComponent,
    ProfileDetailsComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    MaterialModule,
    SharedModule,
    StudentsListModule
  ]
})
export class ProfileModule { }
