import { Component } from '@angular/core';
import {
  NonNullableFormBuilder,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service';

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

  constructor(
    private fb: NonNullableFormBuilder,
    private authService: AuthService,
     private router: Router
  ) {}

  ngOnInit(): void {}

  onLogin(): void {
    this.authForm.markAllAsTouched();
    if (this.authForm.valid) {
      this.authService.login(this.authForm.getRawValue()).subscribe(() => this.router.navigateByUrl('profile'));
    }
  }

  toggleHidePassword(event: any): void {
    this.hidePassword = !this.hidePassword;
    event.stopPropagation();
  }
}
