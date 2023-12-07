import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { SignupPageComponent } from './signup-page/signup-page.component';
import { NavBarComponent } from './shared/nav-bar/nav-bar.component';
import { MainPageComponent } from './main-page/main-page.component';
import {HttpClientModule} from "@angular/common/http";
import {NgOptimizedImage} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MovieService} from "./shared/services/movie.service";
import {FooterComponent} from "./shared/footer/footer.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatSelectModule} from "@angular/material/select";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatPaginatorModule} from "@angular/material/paginator";
import {MoviePageComponent} from "./movie-page/movie-page.component";
import {RouterLink, RouterLinkActive, RouterModule, RouterOutlet} from "@angular/router";
import {routes} from "./app.routes";
import {ShowsPageComponent} from "./shows-page/shows-page.component";
import {MatInputModule} from "@angular/material/input";

@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    SignupPageComponent,
    MoviePageComponent,
    ShowsPageComponent,
    FooterComponent,
    MainPageComponent,
    NavBarComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatSelectModule,
    FormsModule,
    MatButtonModule,
    MatIconModule,
    MatPaginatorModule,
    RouterModule.forRoot(routes),
    MatInputModule
  ],
  providers: [
    MovieService
  ],
  exports: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
