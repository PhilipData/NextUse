import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home-page',
  imports: [CommonModule, RouterLink, HttpClientModule],
  templateUrl: './home-page.html',
  styleUrl: './home-page.css',
})
export class HomePage implements OnInit {

  //featuredProducts: Product[] = [];
  categories: any[] = []; // Replace `any` with your actual Category model, if applicable
  timesToDuplicate: number[] = Array(4).fill(0); // workaround

  constructor(
    //private productService: ProductService,
    //private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.loadFeaturedProducts();
     this.loadCategories();
  }

  loadFeaturedProducts(): void {
    /*this.productService.getAll().subscribe(
      (products: Product[]) => {
        // Filter out only the featured products if there is an `isFeatured` flag
        // this.featuredProducts = products.filter(product => product.profile);
        this.featuredProducts = products;
      },
      (error) => {
        console.error('Error loading products:', error);
      }
    );
    */
  }

  loadCategories(): void {
    /*
    this.categoryService.getAll().subscribe(
      (categories: any[]) => {
        this.categories = categories; // Replace `any` with your actual Category model
      },
      (error) => {
        console.error('Error loading categories:', error);
      }
    );
    */
  }
}
