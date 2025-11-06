import { Routes } from '@angular/router';
import { Register } from './components/public_pages/register/register';
import { HomePage } from './components/public_pages/home-page/home-page';
import { authGuard, noAuthGuard } from './_utils/auth.guard';

export const routes: Routes = [
  { path: '', component: HomePage },
  { path: 'register', component: Register, canActivate: [noAuthGuard]},

];
