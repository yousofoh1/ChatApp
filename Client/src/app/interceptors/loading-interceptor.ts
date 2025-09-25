import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { delay, finalize, of } from 'rxjs';
import { LayoutService } from '../services/layout/layout-service';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const layoutS = inject(LayoutService);
  layoutS.start();

  // const t = of(null).pipe(delay(1000));

  return next(req).pipe(
    finalize(() => {
      layoutS.stop();
    })
  );
};
