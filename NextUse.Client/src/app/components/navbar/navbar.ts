import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../_services/auth.service';
import { Profile } from '../../_models/profile';
import { User } from '../../_models/user';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, CommonModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar implements OnInit {
  user:User | null = null;
  profile?: Profile | null;
  constructor(public authService:AuthService, private router:Router,) {}

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
