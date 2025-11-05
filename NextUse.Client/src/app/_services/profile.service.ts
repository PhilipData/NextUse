import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Profile } from '../_models/profile';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  private readonly apiUrl = environment.apiUrl + 'Profile';
  constructor(private http: HttpClient) { }

  getAll(): Observable<Profile[]>{
    return this.http.get<Profile[]>(this.apiUrl)
  }

  create(profile: Profile): Observable<Profile> {
    return this.http.post<Profile>(this.apiUrl, profile);
  }

  update(profileId:number, profile: Profile): Observable<Profile> {
    return this.http.put<Profile>(this.apiUrl + '/' + profileId, profile);
  }

  findById(profileId: number): Observable<Profile> {
    return this.http.get<Profile>(this.apiUrl + '/' + profileId);
  }
  delete(profileId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${profileId}`);
  }


  

  // ✅ Fetch the currently logged-in user (NEW METHOD)
  getLoggedInUser(): Observable<Profile> {
    return this.http.get<Profile>(`${this.apiUrl}/me`); // Adjust API endpoint if needed
  }
}
