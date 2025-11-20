import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { Role } from './role.enum';
import { firstValueFrom } from 'rxjs';

export const authGuard: CanActivateFn = async (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (!authService.isAuthenticated()) {
    router.navigate(['/login'], { queryParams: { returnUrl: state.url } }); 
    return false;
  }

  const requiredRoles = route.data['roles'] as string[];

  if (requiredRoles && requiredRoles.length > 0) {
    
    if (authService.isAdmin()) {
      return true; 
    }

    if (authService.isSupport() && requiredRoles.some(role => role === Role.Support)) {
      return true; 
    }

    router.navigate(['/']); 
    return false;
  }

  return true;  
};

export const noAuthGuard: CanActivateFn = (route, state) => { 
  const authService = inject(AuthService);
  const router = inject(Router);
  
  if (authService.isAuthenticated()) {
    router.navigate(['/']);
    return false;
  }

  return true;
};
