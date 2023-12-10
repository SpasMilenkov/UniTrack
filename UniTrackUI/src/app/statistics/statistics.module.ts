import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StatisticsLayoutComponent } from './statistics-layout/statistics-layout.component';
import { PrimeNgModule } from '../shared/prime-ng.module';



@NgModule({
  declarations: [
    StatisticsLayoutComponent
  ],
  imports: [
    CommonModule,
    PrimeNgModule
  ]
})
export class StatisticsModule { }
