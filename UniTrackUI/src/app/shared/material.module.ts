import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { NgModule } from '@angular/core';

@NgModule({
  declarations: [],
  imports: [
    BrowserAnimationsModule,
    MatButtonModule,
    MatIconModule
  ],
  providers: [],
  exports: [
    MatButtonModule,
    MatIconModule
  ],
})
export class MaterialModule {}
