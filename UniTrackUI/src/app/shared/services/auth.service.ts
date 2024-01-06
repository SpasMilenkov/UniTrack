import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { userCredentials } from '../models/user-credentials';
import { SignupData } from '../models/signup-data';
import { ConfirmEmailData } from '../models/confirm-email-data';
import { ResetPasswordData } from '../models/reset-password-data';
import { HttpClient } from '@angular/common/http';
import { Observable, switchMap, tap } from 'rxjs';
import { LocalStorageKeys } from '../enums/local-storage-keys.enum';
import { UserService } from './user.service';
import { Roles } from '../enums/roles.enum';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private http: HttpClient,
    private userService: UserService,
    private router: Router
  ) {}

  login(userCredentials: userCredentials): Observable<any> {
    console.log(userCredentials);
    return this.http
      .post('http://localhost:5036/api/Auth/login', userCredentials, {
        withCredentials: true,
      })
      .pipe(
        tap((user: any) =>
          localStorage.setItem(
            LocalStorageKeys.USER_ROLE,
            JSON.stringify(user.userRole)
          )
        ),
        switchMap((user: any) => {
          switch (user.userRole) {
            case Roles.ADMIN:
              return this.userService.getAdminById(user.userId);
            case Roles.STUDENT:
              return this.userService.getStudentById(user.userId);
            case Roles.TEACHER:
              return this.userService.getTeacherById(user.userId);
            default:
              return this.userService.getUserById(user.userId);
          }
        }),
        tap((user: any) =>
          localStorage.setItem(
            LocalStorageKeys.CURRENT_USER,
            JSON.stringify(user)
          )
        )
      );
  }

  signup(user: SignupData): Observable<any> {
    return this.http.post('http://localhost:5036/api/Auth/register', user, {
      withCredentials: true,
    });
  }

  logout(): Observable<any> {
    return this.http
      .post('http://localhost:5036/api/Auth/logout', { withCredentials: true })
      .pipe(
        tap(() => {
          localStorage.removeItem(LocalStorageKeys.CURRENT_USER);
          localStorage.removeItem(LocalStorageKeys.USER_ROLE);
          this.router.navigate(['auth','login']);
        })
      );
  }

  confirmEmail(confirmEmailData: ConfirmEmailData): void {
    // console.log(confirmEmailData)
  }

  resetPassword(resetPasswordData: ResetPasswordData): void {
    // console.log(resetPasswordData)
  }
}
