import { CommonModule } from '@angular/common';
import { Component, OnInit, Inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../_services/auth.service';
import { Profile } from '../../_models/profile';
import { User } from '../../_models/user';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './navbar.html',
  styleUrls: ['./navbar.css'],
})
export class Navbar implements OnInit {
  user:User | null = null;
  constructor(public authService: AuthService, @Inject(Router) private router: Router) {}
 

  ngOnInit() {
    this.user = this.authService.user;  
  }

  logout() {
    this.authService.logout();
    alert(`You're now logged out`)
    this.router.navigate(['/']);
  }

  isAuthenticated() {    
    return this.authService.isAuthenticated()
  }

  isAdmin() {    
    return this.authService.isAdmin()
  }
}
