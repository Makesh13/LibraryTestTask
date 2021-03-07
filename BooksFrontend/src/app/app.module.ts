import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BooksComponent } from './books/books.component';
import {AUTH_API_URL, LIBRARY_API_URL} from './app-injection-tokens';
import {environment} from '../environments/environment';
import {JwtModule, JwtInterceptor} from '@auth0/angular-jwt';
import {ACCESS_TOKEN_KEY} from './services/auth.service';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from '@angular/common/http';
import {InterceptTokenService} from './services/intercept-token.service';
import {ReactiveFormsModule} from '@angular/forms';
import { EditComponent } from './edit/edit.component';

export function tokenGetter(): any
{
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}

@NgModule({
  declarations: [
    AppComponent,
    BooksComponent,
    EditComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter
      }
    }),
    ReactiveFormsModule
  ],
  providers: [
    {
      provide: AUTH_API_URL,
      useValue: environment.authApi
    },
    {
      provide: LIBRARY_API_URL,
      useValue: environment.libraryApi
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptTokenService,
      multi: true
    },
    /*{
      provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true
    }*/
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
