import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ChartModule } from 'primeng/chart';

@NgModule({
  declarations: [],
  imports: [
    BrowserAnimationsModule,
    ButtonModule,
    ChartModule
  ],
  providers: [],
  exports: [
    BrowserAnimationsModule,
    ButtonModule,
    ChartModule
  ],
})
export class PrimeNgModule {}
