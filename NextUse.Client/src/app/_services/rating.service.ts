import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Rating } from '../_models/rating';

@Injectable({
  providedIn: 'root'
})
export class RatingService {

  private readonly apiUrl = environment.apiUrl + 'Rating';
  constructor(private http: HttpClient) { }

  getAll(): Observable<Rating[]>{
    return this.http.get<Rating[]>(this.apiUrl)
  }

  create(rating: Rating): Observable<Rating> {
    return this.http.post<Rating>(this.apiUrl, rating);
  }

  update(ratingId:number, rating: Rating): Observable<Rating> {
    return this.http.put<Rating>(this.apiUrl + '/' + ratingId, rating);
  }

  findById(ratingId: number): Observable<Rating> {
    return this.http.get<Rating>(this.apiUrl + '/' + ratingId);
  }
}
