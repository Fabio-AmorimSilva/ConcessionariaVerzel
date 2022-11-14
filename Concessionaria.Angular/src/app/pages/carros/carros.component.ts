import { AuthService } from './../../shared/services/auth.service';
import { CarroService } from 'src/app/shared/services/carro.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ApiPaginationResponse } from './../../shared/classes/api/api-pagination-response';
import { lastValueFrom } from 'rxjs';
import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { BaseParams } from 'src/app/shared/classes/params/base-params';
import { ConfirmModalConfig } from 'src/app/shared/components/confirm-modal/classes/confirm-modal-config';
import { Carro } from 'src/app/shared/entities/carro.entity';
import { CarroAdmService } from 'src/app/shared/services/carro-admin.service';
import { ConfirmModalService } from 'src/app/shared/components/confirm-modal/services/confirm-modal-service.service';
import { ChangeDetectorRef} from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-carros',
  templateUrl: './carros.component.html',
  styleUrls: ['./carros.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CarrosComponent implements OnInit {

  data!: ApiPaginationResponse<Carro>;
  search!: FormGroup;
  params: any = ["Nome", "Modelo", "Marca", "Valor", "ValorMaiorQue", "ValorMenorQue"];
  value!: string;
  totalPages!: number;
  isAdmin!: boolean;

  constructor(
    private carroAdmService: CarroAdmService,
    private carroService: CarroService,
    private authService: AuthService,
    private confirmModal: ConfirmModalService,
    private changeRef: ChangeDetectorRef,
    private formBuilder: FormBuilder)
    {
      this.search = this.formBuilder.group({
        param: [null],
        value: [null]
      });
    }

   async ngOnInit(): Promise<void>{
    this.loadData();
    this.VerifyIsAdmin();
   }

   VerifyIsAdmin(): void{
      let role = this.authService.getRole();
      if(role == "Admin"){
        this.isAdmin = true;
      }else{
        this.isAdmin = false;
      }
   }

   async refresh(): Promise<void>{
    await this.loadData();
    this.changeRef.detectChanges();
   }

   async loadData(params = new BaseParams()): Promise<void>{
    this.data = await lastValueFrom(this.carroService.getAllParams(params));
    this.data.info = this.data.info.sort((a, b) => (a.valor> b.valor) ? -1 : 1);
    this.totalPages = this.data.totalPages;
   }

  async loadParam(field: string, target: any): Promise<void> {
    if(target instanceof EventTarget) {
      var elemento = target as HTMLInputElement;
      this.value = elemento.value as string;
    }
    const params = {
      [field]: this.value
    } as BaseParams;
    await this.loadData(params);
  }

  async onDelete(id: number): Promise<void>{
    const data = {
      title: "Deletar carro?"
    } as ConfirmModalConfig;
    this.confirmModal.open(data);
    this.confirmModal.closed.subscribe(async (result: any) => {
      if (result) {
        await lastValueFrom(this.carroAdmService.delete(id));
        await this.refresh();
      }
    });
  }

  async changePage(event: PageEvent): Promise<void>{
    const params = {
      take: event.pageSize,
      skip: event.pageIndex * event.pageSize,
    } as BaseParams;
    await this.loadData(params);
  }

}
