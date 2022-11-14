import { CarroFormComponent } from './pages/carros/carro-form/carro-form.component';
import { HomeComponent } from './shared/components/home/home.component';
import { AdminAuth } from './shared/guards/admin-auth.guard';
import { LoginComponent } from './pages/login/login.component';
import { ApiHttpInterceptor } from './shared/interceptors/api-http.interceptor';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { CarrosComponent } from './pages/carros/carros.component';
import { RegisterComponent } from './pages/register/register.component';

const routes: Routes = [
  {path: 'home', component: HomeComponent, children: [
        {path: 'carros', component: CarrosComponent},
        {path: 'admin',
          canActivate: [AdminAuth],
          children: [
            {path: 'carros/cadastro', component: CarroFormComponent},
            {path: 'carros/cadastro/:id', component: CarroFormComponent}
          ],
        },
      ],
    },
  { path: 'registrar', component: RegisterComponent},
  { path: 'login', component: LoginComponent},
  { path: '**', component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ApiHttpInterceptor,
      multi: true,
    },
  ],
})
export class AppRoutingModule { }
