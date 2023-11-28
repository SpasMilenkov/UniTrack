import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { differentPasswordsValidator } from 'src/app/shared/utils/validators';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss'],
})
export class ResetPasswordComponent implements OnInit {
  authForm = this.fb.group(
    {
      password: this.fb.control(null, [
        Validators.required,
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/),
      ]),
      confirmPassword: this.fb.control(null, Validators.required),
    },
    {
      validators: differentPasswordsValidator(),
    }
  );

  hidePassword = true;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {}

  onLogin(): void {
    this.authForm.markAllAsTouched();
    console.log(this.authForm.value);
  }

  toggleHidePassword(event: any): void{
    this.hidePassword = !this.hidePassword;
    event.stopPropagation();
  }
}
