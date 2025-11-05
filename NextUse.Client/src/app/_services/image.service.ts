import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Image } from '../_models/image';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private readonly apiUrl = environment.apiUrl + 'Image';
  constructor(private http: HttpClient) { }

  getAll(): Observable<Image[]>{
    return this.http.get<Image[]>(this.apiUrl)
  }

  create(image: Image): Observable<Image> {
    return this.http.post<Image>(this.apiUrl, image);
  }

  update(imageId:number, image: Image): Observable<Image> {
    return this.http.put<Image>(this.apiUrl + '/' + imageId, image);
  }

  findById(imageId: number): Observable<Image> {
    return this.http.get<Image>(this.apiUrl + '/' + imageId);
  }
}
