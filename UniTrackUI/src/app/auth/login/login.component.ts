import { Component } from '@angular/core';
import { FormBuilder, NonNullableFormBuilder, Validators } from '@angular/forms';
import { switchMap } from 'rxjs';
import { LocalStorageKeys } from 'src/app/shared/enums/local-storage-keys.enum';
import { AuthService } from 'src/app/shared/services/auth.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  authForm = this.fb.group({
    email: this.fb.control('', [
      Validators.required,
      Validators.pattern(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/),
    ]),
    password: this.fb.control('', Validators.required),
  });
  hidePassword = true;

  constructor(private fb: NonNullableFormBuilder, private authService: AuthService, private userService: UserService) {}

  ngOnInit(): void {}

  onLogin(): void {
    this.authForm.markAllAsTouched();
    if(this.authForm.valid){
      this.authService.login(this.authForm.getRawValue()).pipe(
        switchMap((res) => {
          console.log(res);
          return this.userService.getUserById(3);
        })
      ).subscribe();
    }
  }

  toggleHidePassword(event: any): void{
    this.hidePassword = !this.hidePassword;
    event.stopPropagation();
  }
}
