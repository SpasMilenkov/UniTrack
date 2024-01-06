import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StatisticsLayoutComponent } from './statistics-layout/statistics-layout.component';
import { PrimeNgModule } from '../shared/prime-ng.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    StatisticsLayoutComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    PrimeNgModule
  ]
})
export class StatisticsModule { }
