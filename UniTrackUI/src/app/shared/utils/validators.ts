import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function differentPasswordsValidator(): ValidatorFn {
  return (group: AbstractControl): ValidationErrors | null => {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;

    const differentPasswords = password !== confirmPassword;
    return differentPasswords ? { differentPasswords: true } : null;
  };
}
