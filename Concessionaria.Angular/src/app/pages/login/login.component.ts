import { Errors } from './../../shared/entities/error.entity';
import { UsuarioAuth } from './../../shared/entities/usuario-auth.entity';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { take, lastValueFrom } from 'rxjs';
import { errorHandler } from 'src/app/shared/utils/error-handler';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

  form!: FormGroup;
  authUser!: UsuarioAuth;
  error!: Errors;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private router: Router)
    {
      this.form = this.formBuilder.group({
        nomeUsuario: [null, [Validators.required]],
        senha: [null, [Validators.required]]
      })
    }

  ngOnInit(): void {

  }

  async loginAsync(): Promise<void>{
    try {
      if(this.isFormValid()){
        const data = this.form.value as UsuarioAuth;
        const  { token } = await lastValueFrom (this.authService.loginUser(data).pipe(take(1)));
        this.authService.setToken(token);
        this.router.navigate(['/home']);
      }
    }
    catch({error}){
      errorHandler(error as Errors, this.snackBar);
    }
  }

  isFormValid(): boolean{
    const isValid = this.form.valid;
    if(!isValid)
    {
      this.form.markAllAsTouched();
      this.snackBar.open("Campos inválidos no formulário!", undefined, { duration: 5000});
    }

    return isValid;

  }

}
