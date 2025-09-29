import { Routes } from '@angular/router';
import { authGuard } from './guards/auth-guard';
import { HomeP } from './pages/home-p/home-p';

export const routes: Routes = [
  {
    path: '',
    canActivate: [authGuard],
    children: [
      {
        path: '',
        // pathMatch: 'full',
        component: HomeP,
      },
    ],
  },
  {
    path: '',
    canActivate: [authGuard],
    loadChildren: () =>
      import('./pages/auth/auth-routes').then((m) => m.default),
  },
  {
    path: '**',
    redirectTo: '',
  },
];
