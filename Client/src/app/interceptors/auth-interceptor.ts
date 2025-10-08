import { HttpInterceptorFn } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { inject } from '@angular/core';
import { AuthS } from '../features/auth/services/auth-s';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  let cloned;
  let authS = inject(AuthS);

  if (req.url.includes('api')) {
    cloned = req.clone({ headers: req.headers.set('Authorization', 'Bearer ' + authS.token) });
    return next(cloned).pipe(
      catchError((err) => {
        if (err.status === 401) {
          authS.logout();
        }
        return throwError(() => err);
      })
    );
  }

  return next(req).pipe(
    catchError((err) => {
      if (err.status === 401) {
        authS.logout();
      }
      return throwError(() => err);
    })
  );
};
