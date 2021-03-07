import { Injectable } from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ACCESS_TOKEN_KEY} from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class InterceptTokenService implements HttpInterceptor{

  constructor() { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    console.log(localStorage.getItem(ACCESS_TOKEN_KEY));
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${localStorage.getItem(ACCESS_TOKEN_KEY)}`
      }
    });
    return next.handle(req);
  }
}
