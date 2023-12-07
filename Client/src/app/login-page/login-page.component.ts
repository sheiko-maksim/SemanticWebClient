import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {LoginRequest} from "../shared/types/LoginRequest";
import {LoginResult} from "../shared/types/LoginResult";
import {AuthService} from "../shared/services/auth.service";

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  loginRequest = <LoginRequest>{};
  loginResult = <LoginResult>{};

  private returnUrl = '/main';

  loginForm: FormGroup = new FormGroup({
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
    console.log(this.loginRequest);
    this.resetForm();
  }

  onSubmit() {
    console.log(this.loginForm.valid);
    if (this.loginForm.valid) {
      Object.assign(this.loginRequest, this.loginForm.value);
      this.authService.login(this.loginRequest).subscribe({
        next: (result) => {
          this.loginResult = result;
          if (result.success) {
            this.router.navigate([this.returnUrl]);
          }
        },
        error: (error) => {
          if (error.status == 401 || error.status == 400) {
            this.loginResult = error.error;
            if (error.error.message.toLowerCase() === 'invalid password.') {
              this.loginForm.controls['password'].reset();
            } else {
              this.resetForm();
            }
          }
        },
      });
    }
  }


  resetForm() {
    this.loginRequest = <LoginRequest>{};
    this.loginForm.reset();
  }

}
