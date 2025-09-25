import { HttpInterceptorFn } from '@angular/common/http';
import { LayoutService } from '../services/layout/layout-service';
import { inject } from '@angular/core';

export const languageInterceptor: HttpInterceptorFn = (req, next) => {
  let cloned;
  let layoutService = inject(LayoutService);
 
  if (req.url.includes('api')) {
    cloned = req.clone({ headers: req.headers.set('Accept-Language', layoutService.language()) });
    return next(cloned);
  }

  return next(req);
};
