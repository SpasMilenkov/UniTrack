import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { ButtonModule } from 'primeng/button';

@NgModule({
  declarations: [],
  imports: [
    BrowserAnimationsModule,
    ButtonModule
  ],
  providers: [],
  exports: [
    BrowserAnimationsModule,
    ButtonModule
  ],
})
export class PrimeNgModule {}
