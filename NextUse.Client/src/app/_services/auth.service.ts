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
  private userSubject = new BehaviorSubject<User | null>(null); // Observable stream for user
  private profileSubject = new BehaviorSubject<Profile | null>(null);

  constructor(private http: HttpClient) { }

  public get user(): User | null {
    return this.userSubject.value;
  }

  public get profile(): Profile | null {
    return this.profileSubject.value;
  }

  get profile$(): Observable<Profile | null> {
    return this.profileSubject.asObservable(); // Observable for components to subscribe to
  }

  // Check if the user is authenticated
  isAuthenticated(): boolean {
    return !!this.userSubject.value;
  }

  // Role checks
  isAdmin(): boolean {
    return this.userSubject.value?.role === Role.Admin;
  }

  isSupport(): boolean {
    return this.userSubject.value?.role === Role.Support;
  }

  // Load user information from the API
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
        this.userSubject.next(mappedUser);  // Update the BehaviorSubject with the new user

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

  // Gets run one time at the first run of the website to make sure our useres is loaded, before anything else is being executed
  //(this is to make sure that we always know the user is always presented as null, as it doesn't exists and not because it failed to load the user)

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

  // Map the level from API to the Role enum
  private mapLevelToRole(level: string): Role {
    switch (level) {
      case 'admin': return Role.Admin;
      case 'support': return Role.Support;
      case 'user': return Role.User;
      default: return Role.User;
    }
  }
}
