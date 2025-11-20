import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Category } from '../../../_models/category';
import { Profile } from '../../../_models/profile';
import { User } from '../../../_models/user';
import { AuthService } from '../../../_services/auth.service';
import { CategoryService } from '../../../_services/category.service';
import { ProfileService } from '../../../_services/profile.service';

@Component({
  selector: 'app-admindashboard',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './admindashboard.html',
  styleUrl: './admindashboard.css',
})
export class Admindashboard implements OnInit {
   constructor(private categoryService: CategoryService, private auth: AuthService, private profileService: ProfileService) {}

   
  user: User | null = null;
  showCreateCategory = false; // Controls form visibility
  newCategory = ''; // Holds the new category name
  categories: Category[] = []; // Holds the list of categories
  showEditModal = false; // Controls edit modal visibility
  editCategoryId: number | null = null; // Holds the ID of the category being edited
  editCategoryName = ''; // Holds the name of the category being edited
  profiles: Profile[] = []; // Store user profiles
  currentView: 'categories' | 'users' = 'categories'; // Tracks the current view

 

    ngOnInit(): void {
      this.user = this.auth.user; 
    this.loadCategories(); // Load categories on component initialization
  }

  toggleCreateCategory() {
    this.showCreateCategory = !this.showCreateCategory;
  }

  showUserManagement() {
    this.currentView = 'users';
    this.loadProfiles();
  }

  showCategoryManagement() {
    this.currentView = 'categories';
    this.loadCategories();
  }

  loadProfiles() {
    this.profileService.getAll().subscribe({
      next: (data) => (this.profiles = data),
      error: (err) => console.error('Failed to load profiles:', err)
    });
  }

  // Delete a user profile
  // deleteProfile(profile: Profile) {
  //   if (confirm(`Are you sure you want to delete user "${profile.name}"?`)) {
  //     this.profileService.delete(profile.id).subscribe({
  //       next: () => {
  //         this.profiles = this.profiles.filter(p => p.id !== profile.id);
  //         alert('User deleted successfully!');
  //       },
  //       error: (err) => {
  //         console.error('Failed to delete user:', err);
  //         alert('Failed to delete user. Please try again.');
  //       }
  //     });
  //   }
  // }


toggleBlock(profile: Profile) {
  const action = profile.isBlocked ? 'unblock' : 'block';
  const confirmMsg = `Are you sure you want to ${action} user "${profile.name}"?`;

  if (confirm(confirmMsg)) {
    const request$ = profile.isBlocked
      ? this.profileService.unblockProfile(profile.id)
      : this.profileService.blockProfile(profile.id);

    request$.subscribe({
      next: () => {
        profile.isBlocked = !profile.isBlocked;
        alert(`User ${profile.isBlocked ? 'blocked' : 'unblocked'} successfully!`);
      },
      error: (err) => {
        console.error('Failed to update user status:', err);
        alert('Failed to update user status. Please try again.');
      }
    });
  }
}


  loadCategories() {
    this.showCreateCategory = false;
    this.categoryService.getAll().subscribe({
      next: (data) => (this.categories = data),
      error: (err) => console.error('Failed to load categories:', err)
    });
  }

  saveCategory() {
    if (this.newCategory.trim()) {
      const newCategory: Category = {
        id: 0, // ID will be assigned by the backend
        name: this.newCategory
      };

      this.categoryService.create(newCategory).subscribe({
        next: (createdCategory) => {
          this.categories.push(createdCategory);
          console.log('Category Created:', createdCategory);
          alert(`Category "${createdCategory.name}" created successfully!`);
          this.newCategory = ''; // Reset input field
          this.showCreateCategory = false;
        },
        error: (err) => {
          console.error('Failed to create category:', err);
          alert('Failed to create category. Please try again.');
        }
      });
    } else {
      alert('Please enter a category name!');
    }
  }

  editCategory(category: Category) {
    this.editCategoryId = category.id;
    this.editCategoryName = category.name;
    this.showEditModal = true;
  }

  updateCategory() {
    if (this.editCategoryName.trim() && this.editCategoryId !== null) {
      const updatedCategory: Category = {
        id: this.editCategoryId,
        name: this.editCategoryName
      };

      this.categoryService.update(this.editCategoryId, updatedCategory).subscribe({
        next: (data) => {
          const index = this.categories.findIndex(cat => cat.id === this.editCategoryId);
          if (index !== -1) {
            this.categories[index] = data; // Update the category in the list
          }
          alert('Category updated successfully!');
          this.closeEditModal();
        },
        error: (err) => {
          console.error('Failed to update category:', err);
          alert('Failed to update category. Please try again.');
        }
      });
    } else {
      alert('Please enter a valid category name!');
    }
  }

  deleteCategory(category: Category) {
    if (confirm(`Are you sure you want to delete "${category.name}"?`)) {
      this.categoryService.delete(category.id).subscribe({
        next: () => {
          this.categories = this.categories.filter(cat => cat.id !== category.id);
          alert('Category deleted successfully!');
        },
        error: (err) => {
          console.error('Failed to delete category:', err);
          alert('Failed to delete category. Please try again.');
        }
      });
    }
  }

  closeEditModal() {
    this.showEditModal = false;
    this.editCategoryId = null;
    this.editCategoryName = '';
  }

}
