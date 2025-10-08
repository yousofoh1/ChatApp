import { Routes } from '@angular/router';
import { LoginP } from './pages/login-p/login-p';
import { RegisterP } from './pages/register-p/register-p';
import { ConfirmEmailP } from './pages/confirm-email-p/confirm-email-p';

export default [
  {
    path: 'login',
    component: LoginP,
  },
  {
    path: 'register',
    component: RegisterP,
  },
  {
    path: 'confirm-email',
    component: ConfirmEmailP,
  },
] satisfies Routes;
