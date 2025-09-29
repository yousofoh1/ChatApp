import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth/auth-service';

export const authGuard: CanActivateFn = (route: any, state) => {
  // return true;
  const authService = inject(AuthService);
  const router = inject(Router);
  const isLoggedIn = authService.isLoggedIn;
  const path = route?.path ?? '';

  const publicUrls = ['/login', '/register', '/confirm-email'];

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
      router.navigateByUrl('/login');
      return false;
    }
  }
};
