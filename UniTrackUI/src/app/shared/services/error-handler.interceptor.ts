import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private _snackBar: MatSnackBar) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status >= 400 && error.status < 500) {
          console.error('HTTP Error:', error);
          this._snackBar.open(error?.message || 'An unexpected error occurred.', 'Close', {
            duration: 4000,
            horizontalPosition: 'start',
            verticalPosition: 'bottom',
          });
          return throwError(error);
        } else {
          return throwError('An unexpected error occurred.');
        }
      })
    );
  }
}
