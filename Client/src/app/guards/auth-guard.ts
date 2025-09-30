import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth/auth-service';

export const authGuard: CanActivateFn = (route: any, state) => {
  console.log(state.url);
  // return true;
  const authService = inject(AuthService);
  const router = inject(Router);
  const isLoggedIn = authService.isLoggedIn;

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
