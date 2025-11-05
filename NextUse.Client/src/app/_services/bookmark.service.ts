import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Bookmark } from '../_models/bookmark';

@Injectable({
  providedIn: 'root'
})
export class BookmarkService {

  private readonly apiUrl = environment.apiUrl + 'Bookmark';
  constructor(private http: HttpClient) { }
  
  getAll(): Observable<Bookmark[]>{
    return this.http.get<Bookmark[]>(this.apiUrl)
  }

  create(bookmark: Bookmark): Observable<Bookmark> {
    return this.http.post<Bookmark>(this.apiUrl, bookmark);
  }

  update(bookmarkId:number, bookmark: Bookmark): Observable<Bookmark> {
    return this.http.put<Bookmark>(this.apiUrl + '/' + bookmarkId, bookmark);
  }

  delete(bookmarkId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${bookmarkId}`);
  }

  findById(bookmarkId: number): Observable<Bookmark> {
    return this.http.get<Bookmark>(this.apiUrl + '/' + bookmarkId);
  }

  findByProfileId(profileId: number): Observable<Bookmark[]> {
    return this.http.get<Bookmark[]>(this.apiUrl + '/profile/' + profileId)
  }
  
}
