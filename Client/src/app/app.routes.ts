import { NgModule } from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {MainPageComponent} from "./main-page/main-page.component";
import {AuthGuard} from "./shared/auth/auth.guard";
import {LoginPageComponent} from "./login-page/login-page.component";
import {SignupPageComponent} from "./signup-page/signup-page.component";
import {MoviePageComponent} from "./movie-page/movie-page.component";
import {ShowsPageComponent} from "./shows-page/shows-page.component";

export const routes: Routes = [
  {
    path: '',
    component: MainPageComponent
  },
  {
    path: 'main',
    component: MainPageComponent,
  },
  {
    path: 'movies',
    component: MoviePageComponent,
  },
  {
    path: 'shows',
    component: ShowsPageComponent,
  },
  {
    path: 'login',
    component: LoginPageComponent
  },
  {
    path: 'sign-up',
    component: SignupPageComponent
  },
  {
    path: 'profile',
    component: SignupPageComponent,
    canActivate: [AuthGuard]
  }
];



