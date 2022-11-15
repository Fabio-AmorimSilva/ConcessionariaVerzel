import { Injectable } from '@angular/core';
import { BaseParams } from './../classes/params/base-params';
import { ApiPaginationResponse } from './../classes/api/api-pagination-response';
import { environment } from './../../../environments/environment';
import { Carro } from '../entities/carro.entity';
import { take, Observable, Observer } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CarroService{

  env: string = environment.API;

  constructor(
    protected http: HttpClient) { }

  getImg(id: number): Observable<any>{
    return this.http.get<any>(`${this.env}img/${id}`).pipe(take(1));
  }

  getAllParams(params = new BaseParams()): Observable<ApiPaginationResponse<Carro>>{
    return this.http.get<ApiPaginationResponse<Carro>>(`${this.env}carro`, {params}).pipe(take(1));
  }

}
