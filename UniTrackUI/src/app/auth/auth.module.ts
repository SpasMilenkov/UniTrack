import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthLayoutComponent } from './auth-layout/auth-layout.component';
import { AppRoutingModule } from '../app-routing.module';
import { LoginComponent } from './login/login.component';
import { MaterialModule } from '../shared/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { SignupComponent } from './signup/signup.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';



@NgModule({
  declarations: [
    AuthLayoutComponent,
    LoginComponent,
    SignupComponent,
    ResetPasswordComponent,
    ConfirmEmailComponent
  ],
  exports: [
    AuthLayoutComponent,
    LoginComponent,
    SignupComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    MaterialModule,
    ReactiveFormsModule
  ]
})
export class AuthModule { }
