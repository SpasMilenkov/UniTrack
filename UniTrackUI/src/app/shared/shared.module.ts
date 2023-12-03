import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { MaterialModule } from './material.module';
import { FooterComponent } from './components/footer/footer.component';
import { RouterModule } from '@angular/router';
import { EventCardComponent } from './components/event-card/event-card.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddGradeAbsenceDialogComponent } from './components/add-grade-absence-dialog/add-grade-absence-dialog.component';


@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    EventCardComponent,
    AddGradeAbsenceDialogComponent
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    EventCardComponent,
    AddGradeAbsenceDialogComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
