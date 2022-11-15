import { Carro } from '../entities/carro.entity';
import { take, Observable } from 'rxjs';
import { Enumeration } from '../entities/enumeration.entity';
import { HttpClient } from '@angular/common/http';
import { ApibaseService } from './api-base.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CarroAdmService extends ApibaseService<Carro>{

  constructor(protected override http: HttpClient) {
    super("admin/carro", http)
   }

   putImgCarro(id: number, file: File): Observable<any>{
    const fileToUpload = file as File;
    const img = new FormData();
    img.append('img', fileToUpload);

    return this.http.put(`${this.env}${this.route}/img/${id}`, img).pipe(take(1));
   }

   getTipoCarros(): Observable<Enumeration[]>{
      return this.http.get<Enumeration[]>(`${this.env}${this.route}/tipos-carro`).pipe(take(1));
   }
}
