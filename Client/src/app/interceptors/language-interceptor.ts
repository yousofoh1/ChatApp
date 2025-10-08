import { HttpInterceptorFn } from '@angular/common/http';
import { LayoutS } from '../layouts/layout-s';
import { inject } from '@angular/core';

export const languageInterceptor: HttpInterceptorFn = (req, next) => {
  let cloned;
  let layoutS = inject(LayoutS);
 
  if (req.url.includes('api')) {
    cloned = req.clone({ headers: req.headers.set('Accept-Language', layoutS.language()) });
    return next(cloned);
  }

  return next(req);
};
