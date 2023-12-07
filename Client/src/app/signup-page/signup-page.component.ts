import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {AuthService} from "../shared/services/auth.service";
import {SignUpRequest} from "../shared/types/SignUpRequest";
import {SignUpResult} from "../shared/types/SignUpResult";

@Component({
  selector: 'app-signup-page',
  templateUrl: './signup-page.component.html',
  styleUrls: ['./signup-page.component.css']
})
export class SignupPageComponent {

  signupRequest = <SignUpRequest>{};
  signupResult = <SignUpResult>{};
  showResetMessage = false;
  returnUrl = '/login';

  signupForm: FormGroup = new FormGroup({
    userName: new FormControl('', {
      validators: [Validators.required, Validators.pattern('^[A-Za-z ]+$')],
    }),
    email: new FormControl('', {
      validators: [Validators.required, Validators.email],
    }),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
    ]),
  });

  constructor(
    private router: Router,
    private authService: AuthService,
  ) {
    this.signupForm.reset(this.signupRequest);
  }

  onSubmit() {
    if (this.signupForm.valid) {
      Object.assign(this.signupRequest, this.signupForm.value);
      this.authService.signup(this.signupRequest).subscribe({
        next: (result) => {
          this.signupResult = result;
          if (result.success) {
            this.router.navigate([this.returnUrl]);
          }
        },
        error: (error) => {
          if (!error.error?.success) {
            this.signupResult = error.error;
            this.resetForm();
          }
        },
      });
    }
  }

  resetForm() {
    this.signupRequest = <SignUpRequest>{};
    this.signupForm.reset();
  }

}
