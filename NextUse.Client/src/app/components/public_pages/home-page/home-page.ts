import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Product } from '../../../_models/product';
import { CategoryService } from '../../../_services/category.service';
import { ProductService } from '../../../_services/product.service';

@Component({
  selector: 'app-home-page',
  imports: [CommonModule, RouterLink, HttpClientModule],
  templateUrl: './home-page.html',
  styleUrl: './home-page.css',
})
export class HomePage implements OnInit {

  featuredProducts: Product[] = [];
  categories: any[] = []; // Replace `any` with your actual Category model, if it makes sense
  timesToDuplicate: number[] = Array(4).fill(0); // workaround

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.loadFeaturedProducts();
    //  this.loadCategories();
  }

  loadFeaturedProducts(): void {
    this.productService.getAll().subscribe(
      (products: Product[]) => {

        this.featuredProducts = products;
      },
      (error) => {
        console.error('Error loading products:', error);
      }
    );
    
  }

  loadCategories(): void {
    this.categoryService.getAll().subscribe(
      (categories: any[]) => {
        this.categories = categories; // Replace `any` with your actual Category model
      },
      (error) => {
        console.error('Error loading categories:', error);
      }
    );
    
  }
}
