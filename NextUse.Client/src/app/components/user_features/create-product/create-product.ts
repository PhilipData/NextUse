import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Category } from '../../../_models/category';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../_services/auth.service';
import { CategoryService } from '../../../_services/category.service';
import { ProductService } from '../../../_services/product.service';
import { Profile } from '../../../_models/profile';

@Component({
  selector: 'app-create-product',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './create-product.html',
  styleUrl: './create-product.css',
})
export class CreateProduct {
  profile?:Profile;
  productForm: FormGroup;
  categories:Category[] = []
  imagePreviews: string[] = [];
  imageFiles: File[] = [];
  imageError: string | null = null;

  constructor(private authService:AuthService, private productService:ProductService, private categoryService:CategoryService, private fb:FormBuilder, private router: Router, private toastr: ToastrService) {

    this.authService.getProfileForUser().subscribe(x => this.profile = x);

    this.categoryService.getAll().subscribe(x => this.categories = x);

    this.productForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(30), Validators.pattern('^[A-Za-zÀ-ÖØ-öø-ÿ\\s]+$')]],
      description: ['', [Validators.required]],
      price: ['', [Validators.required, Validators.min(0), Validators.max(1000000)]],
      country: ['', [Validators.required, Validators.pattern('^[A-Za-zÀ-ÖØ-öø-ÿ]+$')]],
      city: ['', [Validators.required, Validators.pattern('^[A-Za-zÀ-ÖØ-öø-ÿ]+$')]],
      postalCode: ['', [Validators.required, Validators.pattern('^[0-9]{4,6}$')]],
      category: [null, [Validators.required]]
    });
  }

  onSubmit() {
    if (this.productForm.valid && this.imageFiles.length > 0) {
      const formData = new FormData();
      formData.append('title', this.productForm.get('title')?.value);
      formData.append('description', this.productForm.get('description')?.value);
      formData.append('price', this.productForm.get('price')?.value);
      formData.append('country', this.productForm.get('country')?.value);
      formData.append('city', this.productForm.get('city')?.value);
      formData.append('postalCode', this.productForm.get('postalCode')?.value);
      formData.append('categoryId', this.productForm.get('category')?.value.id);
      formData.append('profileId', this.profile!.id.toString());
      this.imageFiles.forEach(file => formData.append('imageFiles', file));
      
      this.productService.createFullProduct(formData).subscribe((response) => {
        this.toastr.success('Product created!');

        this.router.navigate(['/products', response.id])
      })
    } else {
      this.imageError = 'Please upload at least one image.';
    }
  }

  onImageSelected(event: any) {
    const files = event.target.files;

    if (this.imagePreviews.length >= 8 || (files.length + this.imagePreviews.length > 8)) {
      this.imageError = 'You can only upload a maximum of 8 images.';
      return;
    }

    if (files.length > 0) {
      this.imageError = null;

      for (let file of files) {
        if (!file.type.startsWith('image/')) {
          this.imageError = 'Only image files are allowed!';
          return;
        }
        if (file.size > 2 * 1024 * 1024) { // 2MB limit
          this.imageError = 'Each image must be less than 2MB.';
          return;
        }

        this.imageFiles.push(file);

       
        const reader = new FileReader();
        reader.onload = (e: any) => {
          this.imagePreviews.push(e.target.result);
        };
        reader.readAsDataURL(file);
      }
    }
  }

  removeImage(index: number) {
    this.imagePreviews.splice(index, 1);
    this.imageFiles.splice(index, 1);
  }
}

