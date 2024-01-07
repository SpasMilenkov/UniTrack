import { Component, OnInit } from '@angular/core';
import { FormBuilder, NonNullableFormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';
import { differentPasswordsValidator } from 'src/app/shared/utils/validators';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss'],
})
export class ResetPasswordComponent implements OnInit {
  authForm = this.fb.group(
    {
      password: this.fb.control('', [
        Validators.required,
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/),
      ]),
      confirmPassword: this.fb.control('', Validators.required),
    },
    {
      validators: differentPasswordsValidator(),
    }
  );

  hidePassword = true;

  constructor(private fb: NonNullableFormBuilder, private authService: AuthService) {}

  ngOnInit(): void {}

  onLogin(): void {
    this.authForm.markAllAsTouched();
    if(this.authForm.valid){
      this.authService.resetPassword(this.authForm.getRawValue());
    }
  }

  toggleHidePassword(event: any): void{
    this.hidePassword = !this.hidePassword;
    event.stopPropagation();
  }
}
