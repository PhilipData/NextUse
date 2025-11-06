import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Bookmark } from '../../../_models/bookmark';
import { Category } from '../../../_models/category';
import { Product } from '../../../_models/product';
import { Profile } from '../../../_models/profile';
import { AuthService } from '../../../_services/auth.service';
import { BookmarkService } from '../../../_services/bookmark.service';
import { CategoryService } from '../../../_services/category.service';
import { ProductService } from '../../../_services/product.service';
import { SortOrder } from '../../../_utils/sort.enum';

@Component({
  selector: 'app-product-list',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './product-list.html',
  styleUrl: './product-list.css',
})
export class ProductList {

  constructor(private authService:AuthService, private productService:ProductService, private categoryService:CategoryService, private bookmarkService:BookmarkService) {}

  profile!: Profile;

  public SortOrder = SortOrder;

  products:Product[] = []
  filteredProducts: Product[] = [];
  categories:Category[] = []
  selectedCategory: Category | null = null;
  selectedSort: SortOrder = SortOrder.None;
  searchQuery: string = '';
  bookmarks: Bookmark[] = [];
  showOnlyBookmarked: boolean = false;
  
  
  ngOnInit(): void {
    this.productService.getAll().subscribe(x => {
      this.products = x;
      this.updateFilteredProducts();      
    })
    this.categoryService.getAll().subscribe(x => this.categories = x)

    this.authService.getProfileForUser().subscribe(profile => {
      this.profile = profile

      this.bookmarkService.findByProfileId(this.profile.id).subscribe(bookmarks => {
        if (this.bookmarks)
          this.bookmarks = bookmarks
      })
    })
  }

  updateFilteredProducts() {
    let filteredByCategory = this.selectedCategory
      ? this.products.filter(product => product.category?.id === this.selectedCategory!.id)
      : [...this.products];

    // Filter by search query
    filteredByCategory = filteredByCategory.filter(product => 
      product.title.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
      (product.description && product.description.toLowerCase().includes(this.searchQuery.toLowerCase()))
    );

    // If we are showing only bookmarked products, filter by bookmarks
    if (this.showOnlyBookmarked) {
      filteredByCategory = filteredByCategory.filter(product => 
        this.bookmarks.some(bookmark => bookmark.product?.id === product.id)
      );
    }

    this.filteredProducts = filteredByCategory;

    // Apply sorting after filtering
    this.sortProducts();
  }

  toggleBookmarkedView() {
    this.showOnlyBookmarked = !this.showOnlyBookmarked;
    this.updateFilteredProducts();
  }

  sortProducts() {
    switch (this.selectedSort) {
      case SortOrder.PriceLowToHigh:
        this.filteredProducts.sort((a, b) => a.price - b.price);
        break;
      case SortOrder.PriceHighToLow:
        this.filteredProducts.sort((a, b) => b.price - a.price);
        break;
      default:
        break;
    }
  }
}
