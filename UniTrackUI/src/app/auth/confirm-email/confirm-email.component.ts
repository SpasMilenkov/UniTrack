import { Component } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss'],
})
export class ConfirmEmailComponent {
  authForm = this.fb.group({
    email: this.fb.control('', [
      Validators.required,
      Validators.pattern(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/),
    ]),
  });

  constructor(
    private fb: NonNullableFormBuilder,
    private authService: AuthService
  ) {}

  onSubmit(): void {
    this.authForm.markAllAsTouched();
    if (this.authForm.valid) {
      this.authService.confirmEmail(this.authForm.getRawValue());
    }
  }
}
