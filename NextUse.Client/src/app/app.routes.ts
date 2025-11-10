import { Routes } from '@angular/router';
import { Register } from './components/public_pages/register/register';
import { HomePage } from './components/public_pages/home-page/home-page';
import { authGuard, noAuthGuard } from './_utils/auth.guard';
import { Login } from './components/public_pages/login/login';
import { ProductList } from './components/public_pages/product-list/product-list';
import { CreateProduct } from './components/user_features/create-product/create-product';
import { PersonalProfile } from './components/user_features/personal-profile/personal-profile';
import { Admindashboard } from './components/Admin_Features/admindashboard/admindashboard';
import { Role } from './_utils/role.enum';
import { ProductDetails } from './components/public_pages/product-details/product-details';
import { ChatWidget } from './components/public_pages/chat-widget/chat-widget';


export const routes: Routes = [
    { path: '', component: HomePage },
    { path: 'home', component: HomePage },
    { path: 'login', component: Login, },
    { path: 'register', component: Register, canActivate: [noAuthGuard] },
    { path: 'products', component: ProductList },
    { path: 'product/:productId', component: ProductDetails },
    { path:'admindashboard', component: Admindashboard, canActivate: [authGuard], data: { roles: [Role.Admin] }},
    { path: 'messages', component: ChatWidget, canActivate: [authGuard]},
    { path: 'profile/:profileId', component: PersonalProfile },
    { path: 'products/create', component: CreateProduct, canActivate: [authGuard] },


];
