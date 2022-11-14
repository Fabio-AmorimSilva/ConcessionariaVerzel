import { UsuarioAuth } from './../entities/usuario-auth.entity';
import { TokenProps } from './../classes/auth/token-props';
import { Observable } from 'rxjs';
import { JwtToken } from './../classes/auth/jwt-token';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import jwtDecode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly env = `${environment.API}login`;

  constructor(private http: HttpClient) { }

  loginUser(user: UsuarioAuth) : Observable<JwtToken>{
    return this.http.post<JwtToken>(`${this.env}`, user);
  }

  setToken(token: string) : void{
    const { role } = jwtDecode(token) as TokenProps;
    window.localStorage.setItem("@token", token);
    window.localStorage.setItem("@role", role);

  }

  getRole(){
    return window.localStorage.getItem("@role");
  }

  getToken(){
    return window.localStorage.getItem("@token");
  }

  logout(){
    window.localStorage.removeItem("@token");
    window.localStorage.removeItem("@role");
  }

}
