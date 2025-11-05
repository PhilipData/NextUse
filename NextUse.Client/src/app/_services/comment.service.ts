import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { UserComment } from '../_models/comment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  private readonly apiUrl = environment.apiUrl + 'Comment';
  constructor(private http: HttpClient) { }

  getAll(): Observable<UserComment[]>{
    return this.http.get<UserComment[]>(this.apiUrl)
  }

  create(comment: UserComment): Observable<UserComment> {
    return this.http.post<UserComment>(this.apiUrl, comment);
  }

  update(commentId:number, comment: UserComment): Observable<UserComment> {
    return this.http.put<UserComment>(this.apiUrl + '/' + commentId, comment);
  }

  findById(commentId: number): Observable<UserComment> {
    return this.http.get<UserComment>(this.apiUrl + '/' + commentId);
  }
}
