import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthS } from '../features/auth/services/auth-s';

export const authGuard: CanActivateFn = (route: any, state) => {
  console.log(state.url);
  // return true;
  const authS = inject(AuthS);
  const router = inject(Router);
  const isLoggedIn = authS.isLoggedIn;

  const publicUrls = ['/auth/login', '/auth/register', '/auth/confirm-email'];
  if (isLoggedIn) {
    if (publicUrls.includes(state.url)) {
      router.navigateByUrl('/');
      return false;
    } else {
      return true;
    }
  } else {
    if (publicUrls.includes(state.url)) {
      return true;
    } else {
      router.navigateByUrl('/auth/login');
      return false;
    }
  }
};
