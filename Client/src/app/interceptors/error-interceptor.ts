import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
// import { ToastrService } from 'ngx-toastr';

import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  // const toast = inject(ToastrService);
  const skipError = req.headers.get('skip-error');

  if (skipError === 'true') {
    return next(req);
  }

  return next(req).pipe(
    catchError((error: any) => {
      let message = 'حدث خطأ في الخادم';

      switch (error.status) {
        case 0:
          message = 'لا يوجد اتصال بالانترنت';
          break;
        case 400:
        case 500:
          const err = error.error;

          try {
            const parsedError = typeof err === 'string' ? JSON.parse(err) : err;

            if (parsedError?.errors && typeof parsedError.errors === 'object') {
              const validationMessages: string[] = [];

              for (const key in parsedError.errors) {
                if (parsedError.errors[key] && Array.isArray(parsedError.errors[key])) {
                  validationMessages.push(...parsedError.errors[key]);
                }
              }

              if (validationMessages.length) {
                // Show all validation messages separately or combined
                // validationMessages.forEach((msg) => toast.error(msg));
                return throwError(() => error);
              }
            }

            const outerMessage = parsedError?.message || parsedError?.Message;
            const innerExceptionMessage = parsedError?.exception?.Message || parsedError?.exceptionMessage;
            message = innerExceptionMessage ?? outerMessage ?? 'حدث خطأ في الخادم';
          } catch {
            message = 'حدث خطأ في الخادم';
          }

          break;
        case 401:
          // message = 'يجب تسجيل الدخول اولا';
          //do nothing
          return throwError(() => error);
          break;
        case 403:
          message = 'لا تملك الصلاحية المطلوبة لهذه العملية';
          break;
        case 404:
          message = 'لا يوجد بيانات';
          break;
      }

      //check if toast has max opened 3
      // if (toast.currentlyActive == 0) {
      //   toast.error(message);
      // }
      return throwError(() => error);
    })
  );
};
