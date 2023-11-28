import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { differentPasswordsValidator } from 'src/app/shared/utils/validators';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {
  authForm = this.fb.group(
    {
      email: this.fb.control(null, [
        Validators.required,
        Validators.pattern(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/),
      ]),
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
