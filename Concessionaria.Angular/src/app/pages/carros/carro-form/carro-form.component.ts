import { Errors } from './../../../shared/entities/error.entity';
import { errorHandler } from 'src/app/shared/utils/error-handler';
import { lastValueFrom } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Carro } from 'src/app/shared/entities/carro.entity';
import { Enumeration } from 'src/app/shared/entities/enumeration.entity';
import { ActivatedRoute, Router } from '@angular/router';
import { CarroAdmService } from 'src/app/shared/services/carro-admin.service';

@Component({
  selector: 'app-carro-form',
  templateUrl: './carro-form.component.html',
  styleUrls: ['./carro-form.component.scss']
})
export class CarroFormComponent implements OnInit {

  title!: string;
  form!: FormGroup;
  carro!: Carro;
  id?:number;
  tipoCarros!: Enumeration[];
  response!: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private carroAdmService: CarroAdmService,
    private snackBar: MatSnackBar){}

    async ngOnInit(): Promise<void> {
      this.createForm();
      this.loadData();
      this.setTipoCarros();
    }

    async loadData(): Promise<void>{
      this.id = this.route.snapshot.params['id'];

      if(this.id){
        //Edit Mode
        this.carroAdmService.getById(this.id).subscribe(result => {
          this.carro = result as Carro;
          this.title = "Edit - " + this.carro.nome;

          //Update the form with the user value
          this.form.patchValue(this.carro);
          this.form.get("idTipoCarro")?.setValue(this.carro.tipoCarro.id);
        })
      }else{
        // New Mode
        this.title = "Cadastrar novo carro";
      }
    }

    async createAsync(): Promise<void>{

      var carro = (this.id) ? this.carro : <Carro>{};

      carro.nome = this.form.get("nome")?.value;
      carro.marca = this.form.get("marca")?.value;
      carro.modelo = this.form.get("modelo")?.value;
      carro.valor = this.form.get("valor")?.value;
      carro.foto = this.form.get("foto")?.value;
      carro.idTipoCarro = this.form.get("idTipoCarro")?.value;

      try{
        if(this.isFormValid()){
          if(this.id){
            //Edit Mode
            await lastValueFrom(this.carroAdmService.update(carro, this.id));
            this.router.navigate(['/home/carros']);
          }else{
            //Add Mode
            const data = this.form.value as Carro;
            await lastValueFrom(this.carroAdmService.create(data));
            this.router.navigate(['/home/carros']);
          }
        }
      }
      catch({error}){
        errorHandler(error as Errors, this.snackBar);
      }
    }

  async setTipoCarros(): Promise<void>{
    this.tipoCarros = await lastValueFrom(this.carroAdmService.getTipoCarros());
  }

  createForm(){
    this.form = this.formBuilder.group
      ({
          nome: [null, [Validators.required]],
          marca: [null, [Validators.required]],
          modelo: [null, [Validators.required]],
          valor: [null, [Validators.required]],
          foto: [""],
          idTipoCarro: [null, [Validators.required]]
      });
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
