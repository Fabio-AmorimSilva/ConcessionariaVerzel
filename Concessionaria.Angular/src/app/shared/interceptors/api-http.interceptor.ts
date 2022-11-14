import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Observable } from "rxjs";
import { AuthService } from "../services/auth.service";

@Injectable({
  providedIn: 'root'
})

export class ApiHttpInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService){}

  intercept(req: HttpRequest<any>, next: HttpHandler):
    Observable<HttpEvent<any>>
  {
   const token = this.authService.getToken();
   if(token != null)
   {
      req = req.clone
      ({
          setHeaders: {Authorization: `Bearer ${token}`}
      });
   }

   return next.handle(req);

  }

}
