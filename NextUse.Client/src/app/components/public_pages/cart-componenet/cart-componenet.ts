import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { CartResponse, CartItemResponse } from '../../../_models/cart';
import { CartService } from '../../../_services/cart.service';
import { CommonModule, DecimalPipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cart-componenet',
  imports: [ DecimalPipe, CommonModule ],
  templateUrl: './cart-componenet.html',
  styleUrl: './cart-componenet.css',
})
export class CartComponenet implements OnInit {
  // private cartService = inject(CartService);
   constructor( public cartService: CartService, private toastr: ToastrService) {}

  cart = signal<CartResponse | null>(null);
  loading = signal<boolean>(false);
  error = signal<string | null>(null);

  total = computed(() => this.cart()?.items?.reduce((sum, i) => sum + i.unitPrice * i.quantity, 0) ?? 0);

  ngOnInit(): void {
    this.refresh();
  }

  refresh(): void {
    this.loading.set(true);
    this.error.set(null);
    this.cartService.load().subscribe({
      next: cart => {
        this.cart.set(cart);
        this.loading.set(false);
      },
      error: err => {
        this.error.set('Kunne ikke hente kurven.');
        this.loading.set(false);
        console.error(err);
      }
    });
  }

  inc(item: CartItemResponse): void {
    this.setQty(item, item.quantity + 1);
  }

  dec(item: CartItemResponse): void {
    if (item.quantity > 1) this.setQty(item, item.quantity - 1);
  }

  setQty(item: CartItemResponse, qty: number): void {
    this.loading.set(true);
    this.cartService.updateItem(item.id, qty).subscribe({
      next: cart => {
        this.cart.set(cart);
        this.loading.set(false);
      },
      error: err => {
        this.error.set('Kunne ikke opdatere antal.');
        this.loading.set(false);
        console.error(err);
      }
    });
  }

  remove(item: CartItemResponse): void {
    this.loading.set(true);
    this.cartService.removeItem(item.id).subscribe({
      next: cart => {
        this.cart.set(cart);
        this.loading.set(false);
      },
      error: err => {
        this.error.set('Kunne ikke fjerne vare.');
        this.loading.set(false);
        console.error(err);
      }
    });
  }

  clear(): void {
    if (!confirm('Tøm hele kurven?')) return;
    this.loading.set(true);
    this.cartService.clear().subscribe({
      next: cart => {
        this.cart.set(cart);
        this.loading.set(false);
      },
      error: err => {
        this.error.set('Kunne ikke tømme kurven.');
        this.loading.set(false);
        console.error(err);
      }
    });
  }

checkout(): void {
  if (!confirm('Er du sikker på, at du vil gennemføre checkout?')) return;

  this.loading.set(true);
  this.cartService.checkout().subscribe({
    next: cart => {
      this.cart.set(cart); // frontend cart becomes empty
      this.loading.set(false);
      this.toastr.success('Checkout gennemført! Din ordre er nu CheckedOut.');
    },
    error: err => {
      this.error.set('Checkout fejlede.');
      this.loading.set(false);
      console.error(err);
      this.toastr.error('Checkout fejlede.');
    }
  });
}

}