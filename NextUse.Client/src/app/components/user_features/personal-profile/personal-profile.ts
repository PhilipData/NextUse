import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { firstValueFrom } from 'rxjs';
import { Product } from '../../../_models/product';
import { Profile } from '../../../_models/profile';
import { AuthService } from '../../../_services/auth.service';
import { ProductService } from '../../../_services/product.service';
import { ProfileService } from '../../../_services/profile.service';

@Component({
  selector: 'app-personal-profile',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './personal-profile.html',
  styleUrl: './personal-profile.css',
})
export class PersonalProfile {

  profile: Profile | null = null;
  loggedInProfile: Profile | null = null;
  profileProducts: Product[] = []
  isOwner: Boolean = false;
  starWidth: string = '0%';

  constructor(private authService: AuthService, private activatedRoute: ActivatedRoute, private profileService: ProfileService, private productService:ProductService, private toastr: ToastrService) { }

  async ngOnInit() {
    this.loggedInProfile = await firstValueFrom(this.authService.getProfileForUser());
    this.loadprofile();
  }


  loadprofile() {
    this.activatedRoute.params.subscribe((params => {
      this.profileService.findById(params['profileId']).subscribe(p => {
        this.profile = p
        
        this.isOwner = this.profile.id === this.loggedInProfile?.id
        this.starWidth = `${(this.profile.averageRating! / 5) * 100}%`;

        this.productService.findByProfileId(this.profile.id).subscribe(products => this.profileProducts = products);
      })
    }))
  }

  deleteBookmark(index: number) {
    if (this.profile?.bookmarks) {
      this.profile.bookmarks.splice(index, 1);
    }
  }

  goToBookmark(bookmark: any) {
    console.log('Navigating to:', bookmark);
  }

  editAdvert(advert: any) {
    console.log('Editing Advert:', advert);
  }

  deleteProduct(productId: number) {
    this.productService.deleteById(productId).subscribe(() => {
      this.profileProducts = this.profileProducts.filter(product => product.id !== productId);

      this.toastr.success('Product deleted!');
    })
  }
}
