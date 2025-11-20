import { Injectable } from '@angular/core';
import { Product } from '../_models/product';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { DomSanitizer } from '@angular/platform-browser';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private readonly apiUrl = environment.apiUrl + 'Product';
  constructor(private http: HttpClient, private sanitizer: DomSanitizer) { }

  getAll(): Observable<Product[]>{
    return this.http.get<Product[]>(this.apiUrl).pipe(
      map(products =>
        products.map(product => ({
          ...product,
          images: product.images 
            ? product.images.map(image => ({
                ...image,
                blobUrl: this.createImageUrl(image.blob)
              }))
            : [] 
        }))
      )
    );
  }

  create(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }

  createFullProduct(fullProductForm:any): Observable<Product> {
    return this.http.post<Product>(this.apiUrl + "/full-product", fullProductForm);
  }

  update(productId:number, product: Product): Observable<Product> {
    return this.http.put<Product>(this.apiUrl + '/' + productId, product);
  }

  deleteById(productId:number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${productId}`);
  }

  findById(productId: number): Observable<Product> {
    return this.http.get<Product>(this.apiUrl + '/' + productId).pipe(
      map(product => ({
          ...product,
          images: product.images 
            ? product.images.map(image => ({
                ...image,
                blobUrl: this.createImageUrl(image.blob)
              }))
            : [] 
        }))
    );
  }

  findByProfileId(profileId: number): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl + '/profile/' + profileId).pipe(
      map(products =>
        products.map(product => ({
          ...product,
          images: product.images 
            ? product.images.map(image => ({
                ...image,
                blobUrl: this.createImageUrl(image.blob)
              }))
            : []
        }))
      )
    );
  }

  private createImageUrl(base64Data: string | null): any {
    if (!base64Data) return null;
    const objectURL = `data:image/png;base64,${base64Data}`;
    return this.sanitizer.bypassSecurityTrustUrl(objectURL);
  }
}
