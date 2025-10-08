import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { delay, finalize, of } from 'rxjs';
import { LayoutS } from '../layouts/layout-s';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const layoutS = inject(LayoutS);
  layoutS.start();

  // const t = of(null).pipe(delay(1000));

  return next(req).pipe(
    finalize(() => {
      layoutS.stop();
    })
  );
};
