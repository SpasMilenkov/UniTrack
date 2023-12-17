import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { userCredentials } from '../models/user-credentials';
import { SignupData } from '../models/signup-data';
import { ConfirmEmailData } from '../models/confirm-email-data';
import { ResetPasswordData } from '../models/reset-password-data';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  constructor(private http: HttpClient){}

  login(userCredentials: userCredentials): Observable<any>{
    console.log(userCredentials);
    return this.http.post('http://localhost:5036/api/Auth/login', userCredentials, {withCredentials: true});
  }

  signup(user: SignupData): Observable<any>{
    console.log(user);
    return this.http.post('http://localhost:5036/api/Auth/register', user, {withCredentials: true});
  }

  confirmEmail(confirmEmailData: ConfirmEmailData): void {
    // console.log(confirmEmailData)
  }

  resetPassword(resetPasswordData: ResetPasswordData): void {
    // console.log(resetPasswordData)
  }
}
