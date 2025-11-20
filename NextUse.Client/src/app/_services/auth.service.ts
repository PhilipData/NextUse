import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { User } from '../_models/user';
import { BehaviorSubject, firstValueFrom, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Role } from '../_utils/role.enum';
import { Profile } from '../_models/profile';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiUrl = environment.apiUrl + 'User';
  private userSubject = new BehaviorSubject<User | null>(null); 
  private profileSubject = new BehaviorSubject<Profile | null>(null);

  constructor(private http: HttpClient) { }

  public get user(): User | null {
    return this.userSubject.value;
  }

  public get profile(): Profile | null {
    return this.profileSubject.value;
  }

  get profile$(): Observable<Profile | null> {
    return this.profileSubject.asObservable(); 
  }

  // Check if the user is authenticated
  isAuthenticated(): boolean {
    return !!this.userSubject.value;
  }

 
  isAdmin(): boolean {
    return this.userSubject.value?.role === Role.Admin;
  }

  isSupport(): boolean {
    return this.userSubject.value?.role === Role.Support;
  }

 
  async loadUser(): Promise<void> {
    try {
      const user = await firstValueFrom(this.http.get<any>(this.apiUrl, {withCredentials: true}));
      
      if (user) {
        const mappedUser: User = {
          id: user['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
          username: user['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
          email: user['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
          role: this.mapLevelToRole(user['level'])
        };
        this.userSubject.next(mappedUser);  

        this.loadProfile();
      }
    }
    catch (error) {
      this.userSubject.next(null);
    }

  }

  loadProfile(): void {
    this.http.get<Profile>(this.apiUrl + "/profile", { withCredentials: true }).subscribe(
      profile => this.profileSubject.next(profile),
      () => this.profileSubject.next(null)
    );
  }


  initializeApp(): Promise<void> {
    return this.loadUser();
  }

  login(loginForm: any) {
    return this.http.post<any>(this.apiUrl + "/login", loginForm, {withCredentials: true})
  }

  register(registerForm: any) {
    return this.http.post<any>(this.apiUrl + "/register", registerForm, {withCredentials: true})
  }

  logout(): void {
    this.http.get(this.apiUrl + "/logout", { withCredentials: true }).subscribe(() => {
       this.userSubject.next(null);
       this.profileSubject.next(null);
    });
  }

  getProfileForUser(): Observable<Profile> {
    return this.http.get<Profile>(this.apiUrl + "/profile", {withCredentials: true})
  }

  private mapLevelToRole(level: string): Role {
    switch (level) {
      case 'admin': return Role.Admin;
      case 'support': return Role.Support;
      case 'user': return Role.User;
      default: return Role.User;
    }
  }
}
