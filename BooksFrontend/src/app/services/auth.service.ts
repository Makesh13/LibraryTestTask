import {Inject, Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {Token} from '../models/token';
import {AUTH_API_URL} from '../app-injection-tokens';
import {HttpClient} from '@angular/common/http';
import {JwtHelperService} from '@auth0/angular-jwt';
import {Router} from '@angular/router';
import {tap} from 'rxjs/operators';

export const ACCESS_TOKEN_KEY = 'access_token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    @Inject(AUTH_API_URL) private apiUrl: string,
    private httpClient: HttpClient,
    private jwtHelper: JwtHelperService,
    private route: Router
  ) { }

  login(username: string, password: string): Observable<Token>
  {
    return this.httpClient.post<Token>(`${this.apiUrl}api/auth/login`, {username, password}).
      pipe(
        tap(token => {
          localStorage.setItem(ACCESS_TOKEN_KEY, token.token);
        })
    );
  }

  isAuthenticated(): boolean
  {
    const token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return token && !this.jwtHelper.isTokenExpired(token) && this.jwtHelper.decodeToken(token).role === 'Admin';
  }

  logout(): void
  {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.route.navigate(['']);
  }
}
