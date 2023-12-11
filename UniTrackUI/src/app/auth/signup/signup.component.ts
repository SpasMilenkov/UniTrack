import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { User } from 'src/app/shared/models/user';
import { AuthService } from 'src/app/shared/services/auth.service';
import { differentPasswordsValidator } from 'src/app/shared/utils/validators';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {
  authForm = this.fb.group(
    {
      firstName: this.fb.control('', Validators.required),
      lastName: this.fb.control('', Validators.required),
      userName: this.fb.control('', Validators.required),
      email: this.fb.control('', [
        Validators.required,
        Validators.pattern(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/),
      ]),
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

  constructor(
    private fb: FormBuilder,
    private authService: AuthService
  ) {}

  ngOnInit(): void {}

  onSignup(): void {
    this.authForm.markAllAsTouched();
    console.log(this.authForm.value);

    if(this.authForm.valid){
      this.authService.signup(this.authForm.getRawValue() as User);
    }
  }

  toggleHidePassword(event: any): void{
    this.hidePassword = !this.hidePassword;
    event.stopPropagation();
  }
}
