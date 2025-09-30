import { Routes } from '@angular/router';
import { LoginP } from './login-p/login-p';
import { RegisterP } from './register-p/register-p';
import { ConfirmEmailP } from './confirm-email-p/confirm-email-p';

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
