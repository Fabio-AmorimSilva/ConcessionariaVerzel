import { Observable, take } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { Usuario } from '../entities/user.entity';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  env = environment.API;

  constructor(private http: HttpClient) { }

  register(user: Usuario): Observable<Usuario>{
    return this.http.post<Usuario>(`${this.env}usuario`, user).pipe(take(1));
  }

}
