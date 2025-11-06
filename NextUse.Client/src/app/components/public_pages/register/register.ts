import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, NgModelGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../_services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  registerForm: FormGroup;

  constructor(private fb:FormBuilder, private authService:AuthService, private router:Router, private toastr:ToastrService) {
  this.registerForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6), Validators.pattern('^(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z0-9]).{6,}$')]],
    confirmPassword: ['', [Validators.required]],
    name: ['', [Validators.required, Validators.pattern('^[A-Za-zÀ-ÖØ-öø-ÿ]+$')]],
    country: ['', [Validators.required, Validators.pattern('^[A-Za-zÀ-ÖØ-öø-ÿ]+$')]],
    city: ['', [Validators.required, Validators.pattern('^[A-Za-zÀ-ÖØ-öø-ÿ]+$')]],
    postalCode: ['', [Validators.required, Validators.pattern('^[0-9]{4,6}$')]],
    street: ['', [Validators.required, Validators.pattern(`^(?=.*[A-Za-zÀ-ÖØ-öø-ÿ])[A-Za-zÀ-ÖØ-öø-ÿ0-9]+$`)]],
    houseNumber: ['', [Validators.required, Validators.pattern(`^(?=.*\\d)[A-Za-zÀ-ÖØ-öø-ÿ0-9]+$`)]],
  }, { validator: this.matchPasswords('password', 'confirmPassword') });
  }

  onSubmit(){
    if(this.registerForm.valid && this.registerForm.touched){
      this.authService.register(this.registerForm.value).subscribe({
        next: () => {
          this.authService.loadUser();
          
          this.toastr.success('Register Successful! Please login')
          this.router.navigate(['/login'])
        },
        error: (error) =>{
          this.toastr.error(`Error something isn't right`)
          console.error(error)
        }
      })
    }
  }

  matchPasswords(password: string, confirmPassword: string) {
    return (formGroup: FormGroup) => {
      const passControl = formGroup.controls[password];
      const confirmPassControl = formGroup.controls[confirmPassword];

      if (confirmPassControl.errors && !confirmPassControl.errors['mustMatch']) {
        return;
      }

      if (passControl.value !== confirmPassControl.value) {
        confirmPassControl.setErrors({ mustMatch: true });
      } else {
        confirmPassControl.setErrors(null);
      }
    };
  }

}
