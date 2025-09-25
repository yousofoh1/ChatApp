import { HttpInterceptorFn } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth/auth-service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  let cloned;
  let authService = inject(AuthService);

  if (req.url.includes('api')) {
    cloned = req.clone({ headers: req.headers.set('Authorization', 'Bearer ' + authService.token) });
    return next(cloned).pipe(
      catchError((err) => {
        if (err.status === 401) {
          authService.logout();
        }
        return throwError(() => err);
      })
    );
  }

  return next(req).pipe(
    catchError((err) => {
      if (err.status === 401) {
        authService.logout();
      }
      return throwError(() => err);
    })
  );
};
