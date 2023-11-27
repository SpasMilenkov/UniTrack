import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthLayoutComponent } from './auth-layout/auth-layout.component';
import { AppRoutingModule } from '../app-routing.module';
import { MaterialModule } from '../shared/material.module';



@NgModule({
  declarations: [
    AuthLayoutComponent,
  ],
  exports: [
    AuthLayoutComponent,
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    MaterialModule,
  ]
})
export class AuthModule { }
