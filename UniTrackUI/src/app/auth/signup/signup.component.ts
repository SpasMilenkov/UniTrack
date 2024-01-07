import { Component, OnInit } from '@angular/core';
import { FormBuilder, NonNullableFormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { School } from 'src/app/shared/models/school';
import { AdminService } from 'src/app/shared/services/admin.service';
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
      schoolId: this.fb.control('', Validators.required)
    },
    {
      validators: differentPasswordsValidator(),
    }
  );
  hidePassword = true;
  schools$!: Observable<School[]>;

  constructor(
    private fb: NonNullableFormBuilder,
    private authService: AuthService,
    private adminService: AdminService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.schools$ = this.adminService.getAllSchools();
  }

  onSignup(): void {
    this.authForm.markAllAsTouched();
    if(this.authForm.valid){
      this.authService.signup(this.authForm.getRawValue()).subscribe(() => this.router.navigateByUrl('login'));
    }
  }

  toggleHidePassword(event: any): void{
    this.hidePassword = !this.hidePassword;
    event.stopPropagation();
  }
}
