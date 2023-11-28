import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  authForm = this.fb.group({
    email: this.fb.control(null, [
      Validators.required,
      Validators.pattern(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/),
    ]),
    password: this.fb.control(null, Validators.required),
  });

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {}

  onLogin(): void {
    this.authForm.markAllAsTouched();
    console.log(this.authForm.value);
  }
}
