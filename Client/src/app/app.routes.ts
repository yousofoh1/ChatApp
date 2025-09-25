import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./pages/login-p/login-p').then((m) => m.LoginP),
  }
];
