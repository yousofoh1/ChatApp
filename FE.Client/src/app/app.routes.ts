import { Routes } from '@angular/router';
import { MainLayout } from './shared/layouts/main-layout/main-layout';

export const routes: Routes = [
  {
    path: '',
    component: MainLayout,
    children: [
      {
        path: '',
        component: () => import('./pages/home/home.page').then(m => m.HomeModule)
      }
    ]
  },
];
