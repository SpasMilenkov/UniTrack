import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { MaterialModule } from './material.module';
import { FooterComponent } from './components/footer/footer.component';
import { RouterModule } from '@angular/router';
import { EventCardComponent } from './components/event-card/event-card.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddGradeAbsenceDialogComponent } from './components/add-grade-absence-dialog/add-grade-absence-dialog.component';
import { PrimeNgModule } from './prime-ng.module';
import { RecommendedMaterialCardComponent } from './components/recommended-material-card/recommended-material-card.component';


@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    EventCardComponent,
    AddGradeAbsenceDialogComponent,
    RecommendedMaterialCardComponent
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    EventCardComponent,
    AddGradeAbsenceDialogComponent,
    RecommendedMaterialCardComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    PrimeNgModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
