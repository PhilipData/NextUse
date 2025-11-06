import { Routes } from '@angular/router';
import { Register } from './components/public_pages/register/register';
import { HomePage } from './components/public_pages/home-page/home-page';
import { authGuard, noAuthGuard } from './_utils/auth.guard';
import { Login } from './components/public_pages/login/login';
import { noAuthGuard } from './_utils/auth.guard';
import { HomePage } from './components/public_pages/home-page/home-page';

export const routes: Routes = [

    { path: '', component: HomePage },
    { path: 'login', component: Login, },
    { path: 'register', component: Register, canActivate: [noAuthGuard] },
];
