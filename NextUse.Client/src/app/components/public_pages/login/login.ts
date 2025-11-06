import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../_services/auth.service';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { filter, firstValueFrom } from 'rxjs';
import { Profile } from '../../../_models/profile';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
})
export class Login {
  loginForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService,
    private activatedRoute: ActivatedRoute
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }
  onSubmit() {
  if (this.loginForm.valid && this.loginForm.touched) {
    this.authService.login(this.loginForm.value).subscribe({
      next: async () => {
        await this.authService.loadUser();

        const profile = await firstValueFrom(
          this.authService.profile$.pipe(
            filter((p): p is Profile => p !== null)
          )
        );

        this.toastr.success(`Hello, ${profile.name} Welcome!`);

        const returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/';
        this.router.navigate([returnUrl]);
      },
      error: (error) => {
        if (error.status === 403 && error.error?.message === "Your profile is blocked. Contact support.") {
          this.authService.logout(); // Clear auth state completely
          this.toastr.error('Your account is blocked. Please contact support.');
        } else {
          this.authService.logout(); // Also clear on any failed login attempt
          this.toastr.error('Email and/or password is wrong');
        }
        console.error(error);
      }
    });
  }
}
}
