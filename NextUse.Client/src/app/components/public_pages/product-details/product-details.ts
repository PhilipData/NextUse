import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Profile } from '../../../_models/profile';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';
import { Bookmark } from '../../../_models/bookmark';
import { UserComment } from '../../../_models/comment';
import { Product } from '../../../_models/product';
import { AuthService } from '../../../_services/auth.service';
import { BookmarkService } from '../../../_services/bookmark.service';
import { CommentService } from '../../../_services/comment.service';
import { ProductService } from '../../../_services/product.service';

@Component({
  selector: 'app-product-details',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './product-details.html',
  styleUrl: './product-details.css',
})
export class ProductDetails {
   profile!: Profile;
  product?: Product;
  selectedImage: string = '';
  bookmark: Bookmark = this.resetBookmark();
  commentInput: string = '';
  newComment?: UserComment;
  commentError: string = '';
  isBookmarked: boolean = false;
  isOwner:Boolean = false;
  starWidth: string = '0%';

  constructor(
    private productService: ProductService,
    private bookmarkService: BookmarkService,
    private commentService: CommentService,
    private authService: AuthService,
    private router: Router,
    private toastr:ToastrService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.authService.getProfileForUser().pipe(
      finalize(() => {
        // This will always be executed after the observable completes
        this.loadProduct();
      })
    ).subscribe({
      next: (profile) => {
        this.profile = profile;
      }
    });
  }


  setSelectedImage(imageUrl: string) {
    this.selectedImage = imageUrl;
  }

  submitComment() {
    if (!this.commentInput.trim()) {
      this.commentError = 'Comment cannot be empty!';
      return;
    }

    if (this.profile) {
      this.newComment = {
        id: 0,
        content: this.commentInput.trim(),
        productId: this.product?.id,
        profileId: this.profile.id,
        createdAt: '',
      };

      this.commentService.create(this.newComment).subscribe((x) => {
        this.product?.comments.push(x);
        this.commentInput = '';
        this.commentError = '';
      });
    }
  }

  loadProduct(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.productService.findById(params['productId']).subscribe((product) => {
        if (!product) return;
        this.product = product;
        this.selectedImage = product.images[0]?.blobUrl || '';
  
        this.starWidth = `${(product.profile?.averageRating! / 5) * 100}%`;
  
        // Only check bookmarks if profile exists
        if (this.profile) {
          this.bookmarkService.findByProfileId(this.profile.id).subscribe((bookmarks) => {
            let existingBookmark = bookmarks?.find((x) => x.product?.id === this.product?.id);
            this.isBookmarked = !!existingBookmark;
            if (existingBookmark) {
              this.bookmark = existingBookmark;
            }
            this.isOwner = this.profile.id == this.product?.profile?.id
          });
        }
      });
    });
  }

  deleteProduct(productId: number | undefined) {
    if (productId) {
      this.productService.deleteById(productId).subscribe(() => {
        this.toastr.success('Product deleted!');
        
        this.router.navigate(['/profile', this.profile.id])
      })
    }
  }

  toggleBookmark() {
    this.isBookmarked = !this.isBookmarked;

    if (this.isBookmarked) {
      this.bookmark = {
        id: 0,
        productId: this.product?.id,
        profileId: this.profile.id,
      };
      this.bookmarkService.create(this.bookmark).subscribe((newBookmark) => {
        this.bookmark = newBookmark;
      });
    } else {
      this.bookmarkService.delete(this.bookmark.id).subscribe(() => {
        this.bookmark = this.resetBookmark();
      });
    }
  }

  resetBookmark(): Bookmark {
    return { id: 0, productId: 0, profileId: 0 };
  }

}
