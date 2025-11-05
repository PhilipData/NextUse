import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Category } from '../_models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private readonly apiUrl = environment.apiUrl +'Category';
  constructor(private http: HttpClient) { }

  getAll(): Observable<Category[]>{
    return this.http.get<Category[]>(this.apiUrl)
  }

  create(category: Category): Observable<Category> {
    return this.http.post<Category>(this.apiUrl, category);
  }

  update(categoryId:number, category: Category): Observable<Category> {
    return this.http.put<Category>(this.apiUrl + '/' + categoryId, category);
  }

  findById(categoryId: number): Observable<Category> {
    return this.http.get<Category>(this.apiUrl + '/' + categoryId);
  }
  delete(categoryId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${categoryId}`);
  }
}
