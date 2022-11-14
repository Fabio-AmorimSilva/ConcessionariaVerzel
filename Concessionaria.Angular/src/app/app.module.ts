import { NavbarModule } from './shared/components/navbar/navbar.module';
import { HomeModule } from './shared/components/home/home.module';
import { CarroFormModule } from './pages/carros/carro-form/carro-form.module';
import { CarrosModule } from './pages/carros/carros.module';
import { LoginModule } from './pages/login/login.module';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ConfirmModalModule } from './shared/components/confirm-modal/confirm-modal.module';
import { RegisterModule } from './pages/register/register.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NavbarModule,
    HomeModule,
    LoginModule,
    RegisterModule,
    CarrosModule,
    CarroFormModule,
    ConfirmModalModule,
    MatSnackBarModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
