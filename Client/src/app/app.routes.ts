import { Routes } from '@angular/router';
import { authGuard } from './guards/auth-guard';
import { HomeP } from './pages/home-p/home-p';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    canActivate: [authGuard],
    children: [
      {
        path: '',
        component: HomeP,
      },
    ],
  },
  {
    path: 'auth',
    canMatch: [authGuard],
    loadChildren: () =>
      import('./pages/auth/auth-routes').then((m) => m.default),
  },
  {
    path: '**',
    redirectTo: '',
  },
];
