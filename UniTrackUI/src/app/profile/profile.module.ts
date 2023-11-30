import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './profile/profile.component';
import { ProfileDetailsComponent } from './profile-details/profile-details.component';
import { StudentDetailsCardComponent } from './student-details-card/student-details-card.component';



@NgModule({
  declarations: [
    ProfileComponent,
    ProfileDetailsComponent,
    StudentDetailsCardComponent
  ],
  exports: [
    ProfileComponent,
    ProfileDetailsComponent
  ],
  imports: [
    CommonModule
  ]
})
export class ProfileModule { }
