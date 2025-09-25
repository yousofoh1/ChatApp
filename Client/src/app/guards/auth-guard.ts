import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth/auth-service';

export const authGuard: CanActivateFn = (route: any, state) => {
  // return true;
  const authService = inject(AuthService);
  const router = inject(Router);
  const isLoggedIn = authService.isLoggedIn;
  const path = route?.path ?? '';
 
  if (path == 'auth') {
    if (!isLoggedIn) {
      return true;
    } else {
      router.navigateByUrl('/');
      return false;
    }
  }

  // Block all other routes if NOT logged in
  if (!isLoggedIn) {
    router.navigateByUrl('/auth');
    return false;
  }

  // Allow everything else if logged in
  return true;
};
