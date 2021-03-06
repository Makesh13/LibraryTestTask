import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LIBRARY_API_URL} from '../app-injection-tokens';
import {Observable} from 'rxjs';
import {Book} from '../models/book';
import {tap} from 'rxjs/operators';
import {ACCESS_TOKEN_KEY} from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class BooksLibraryService {

  constructor(
    private httpClient: HttpClient,
    @Inject(LIBRARY_API_URL) private librarryApi: string,
  ) { }
  getBooks(): Observable<Book[]>
  {
    return this.httpClient.get<Book[]>(`${this.librarryApi}api/books`);
  }
  AddBook(book: Book): Observable<any>
  {
    const headers: Headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Authorization', 'Bearer ' + localStorage.getItem(ACCESS_TOKEN_KEY));
    return this.httpClient.post<any>(`${this.librarryApi}api/books`, book, );
  }
}
