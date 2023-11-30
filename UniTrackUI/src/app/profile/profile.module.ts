import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './profile/profile.component';
import { ProfileDetailsComponent } from './profile-details/profile-details.component';
import { StudentDetailsCardComponent } from './student-details-card/student-details-card.component';
import { TeacherDetailsCardComponent } from './teacher-details-card/teacher-details-card.component';
import { MaterialModule } from '../shared/material.module';



@NgModule({
  declarations: [
    ProfileComponent,
    ProfileDetailsComponent,
    StudentDetailsCardComponent,
    TeacherDetailsCardComponent
  ],
  exports: [
    ProfileComponent,
    ProfileDetailsComponent
  ],
  imports: [
    CommonModule,
    MaterialModule
  ]
})
export class ProfileModule { }
