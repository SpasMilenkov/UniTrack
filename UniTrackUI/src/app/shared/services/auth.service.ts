import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { userCredentials } from '../models/user-credentials';
import { SignupData } from '../models/signup-data';
import { ConfirmEmailData } from '../models/confirm-email-data';
import { ResetPasswordData } from '../models/reset-password-data';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  login(userCredentials: userCredentials): void{
    console.log(userCredentials)
  }

  signup(user: SignupData): void{
    console.log(user)
  }

  confirmEmail(confirmEmailData: ConfirmEmailData): void {
    console.log(confirmEmailData)
  }

  resetPassword(resetPasswordData: ResetPasswordData): void {
    console.log(resetPasswordData)
  }
}
