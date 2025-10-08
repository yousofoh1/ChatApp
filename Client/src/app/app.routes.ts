import { Routes } from '@angular/router';
import { authGuard } from './guards/auth-guard';
import { HomeP } from './features/general/home-p/home-p';
import { MainLayout } from './layouts/main-layout/main-layout';
import { MessagingP } from './features/messaging/messaging-p/messaging-p';

export const routes: Routes = [
  {
    path: '',
    canActivate: [authGuard],
    component: MainLayout,
    children: [
      {
        path: '',
        pathMatch: 'full',
        component: HomeP,
      },
      {
        path: ':userName',
        component: MessagingP,
      },
    ],
  },
  {
    path: 'auth',
    canActivate: [authGuard],
    loadChildren: () =>
      import('./features/auth/routes').then((m) => m.default),
  },
  {
    path: '**',
    redirectTo: '',
  },
];
