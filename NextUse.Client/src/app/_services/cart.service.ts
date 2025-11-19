import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { CartResponse } from '../_models/cart';

@Injectable({ providedIn: 'root' })
export class CartService {
  private http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl + 'Cart';
  //https://localhost:7116/api/Cart

  private cartSubject = new BehaviorSubject<CartResponse | null>(null);
  cart$ = this.cartSubject.asObservable();

  /** Load/refresh the cart (call on app start or when you need it) */
load(): Observable<CartResponse> {
    return this.http.get<CartResponse>(this.baseUrl, { withCredentials: true }).pipe(
        tap(cart => this.cartSubject.next(cart))
    );
}


addItem(productId: number, quantity = 1): Observable<CartResponse> {
    return this.http.post<CartResponse>(
        `${this.baseUrl}/items`,
        { productId, quantity },
        { withCredentials: true } // send cookies/auth
    ).pipe(
        tap(cart => this.cartSubject.next(cart))
    );
}



updateItem(cartItemId: number, quantity: number): Observable<CartResponse> {
  return this.http.put<CartResponse>(
    `${this.baseUrl}/items`,
    { cartItemId, quantity },
    { withCredentials: true } // <-- ensures cookies/session are sent
  ).pipe(
    tap(cart => this.cartSubject.next(cart))
  );
}


//   removeItem(cartItemId: number): Observable<CartResponse> {
//     return this.http.delete<CartResponse>(`${this.baseUrl}/items/${cartItemId}`).pipe(
//       tap(cart => this.cartSubject.next(cart))
//     );
//   }

//   clear(): Observable<CartResponse> {
//     return this.http.delete<CartResponse>(`${this.baseUrl}/clear`).pipe(
//       tap(cart => this.cartSubject.next(cart))
//     );
//   }

//   checkout(): Observable<CartResponse> {
//     return this.http.post<CartResponse>(`${this.baseUrl}/checkout`, {}).pipe(
//       tap(cart => this.cartSubject.next(cart))
//     );
//   }

removeItem(cartItemId: number): Observable<CartResponse> {
    return this.http.delete<CartResponse>(
        `${this.baseUrl}/items/${cartItemId}`,
        { withCredentials: true }
    ).pipe(
        tap(cart => this.cartSubject.next(cart))
    );
}

clear(): Observable<CartResponse> {
    return this.http.delete<CartResponse>(
        `${this.baseUrl}/clear`,
        { withCredentials: true }
    ).pipe(
        tap(cart => this.cartSubject.next(cart))
    );
}

checkout(): Observable<CartResponse> {
    return this.http.post<CartResponse>(
        `${this.baseUrl}/checkout`,
        {},
        { withCredentials: true }
    ).pipe(
        tap(cart => this.cartSubject.next(cart))
    );
}

  /** Convenient snapshot getter */
  get value(): CartResponse | null {
    return this.cartSubject.value;
  }
}
