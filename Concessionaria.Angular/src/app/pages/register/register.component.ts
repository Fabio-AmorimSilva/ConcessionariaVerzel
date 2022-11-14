import { MatSnackBar } from '@angular/material/snack-bar';
import { lastValueFrom } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
/* eslint-disable @angular-eslint/no-empty-lifecycle-method */
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Errors } from 'src/app/shared/entities/error.entity';
import { errorHandler } from 'src/app/shared/utils/error-handler';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  form!: FormGroup;
  error!: Errors;

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private router: Router,
    private snackBar: MatSnackBar)
    {
      this.form = this.formBuilder.group
      ({
        nome: [null, Validators.required],
        nomeUsuario: [null, Validators.required],
        email: [null, Validators.required],
        senha: [null, Validators.required]
      });
    }

  ngOnInit(): void {
  }

  async createUser(): Promise<void>{
    try{
      if(this.isFormValid()){
        const data = this.form.value;
        await lastValueFrom(this.userService.register(data));
        this.snackBar.open("Usuário registrado com sucesso!!", undefined, {duration: 5000});
        this.router.navigate(['/login']);
      }
    }
    catch({error}){
      errorHandler(error as Errors, this.snackBar);
    }

  }

  isFormValid(): boolean{
    const isValid = this.form.valid;
    if(!isValid){
      this.form.markAllAsTouched();
      this.snackBar.open("Campos inválidos no formulário!!", undefined, {duration: 5000});
      return isValid;
    }
    return isValid;
  }

}
