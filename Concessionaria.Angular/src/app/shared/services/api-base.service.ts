import { ApiPaginationResponse } from './../classes/api/api-pagination-response';
import { BaseParams } from './../classes/params/base-params';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { take, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApibaseService<T> {

  env: string = environment.API;

  constructor(
    @Inject("route") protected route: string,
    protected http: HttpClient) { }

  getAllParams(params = new BaseParams()): Observable<ApiPaginationResponse<T>>{
    return this.http.get<ApiPaginationResponse<T>>(`${this.env}${this.route}`, {params}).pipe(take(1));
  }

  getById(id: number): Observable<T>{
    return this.http.get<T>(`${this.env}${this.route}/${id}`).pipe(take(1));
  }

  create(register: T): Observable<T>{
    return this.http.post<T>(`${this.env}${this.route}`, register).pipe(take(1));
  }

  update(register: T, id: number): Observable<T>{
    return this.http.put<T>(`${this.env}${this.route}/${id}`, register).pipe(take(1));
  }

  delete(id: number): Observable<void>{
    return this.http.delete<void>(`${this.env}${this.route}/${id}`).pipe(take(1));
  }

}
