import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { Role } from './role.enum';
import { firstValueFrom } from 'rxjs';

export const authGuard: CanActivateFn = async (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  // Check if the user is authenticated
  if (!authService.isAuthenticated()) {
    router.navigate(['/login'], { queryParams: { returnUrl: state.url } });  // Redirect to login if not authenticated
    return false;
  }

  const requiredRoles = route.data['roles'] as string[];

  if (requiredRoles && requiredRoles.length > 0) {
    
    if (authService.isAdmin()) {
      return true; // Allow access for Admin role
    }

    // Support role can access Support and User paths
    if (authService.isSupport() && requiredRoles.some(role => role === Role.Support)) {
      return true; // Allow access for Support role to Support paths
    }

    router.navigate(['/']); // If user doesn't have required role, navigate to root
    return false;
  }

  return true;  // Allow access if no role restrictions are defined
};

export const noAuthGuard: CanActivateFn = (route, state) => { // used for paths where authenticated users should not be able to go
  const authService = inject(AuthService);
  const router = inject(Router);
  
  if (authService.isAuthenticated()) {
    router.navigate(['/']);
    return false;
  }

  return true;
};
