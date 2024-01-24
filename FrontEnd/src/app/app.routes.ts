import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./components/list-patient/list-patient.component')
  },
  {
    path: 'detail/:id',
    loadComponent: () => import('./components/detail-patient/detail-patient.component')
  },
  {
    path: 'detail',
    loadComponent: () => import('./components/detail-patient/detail-patient.component')
  }
];
