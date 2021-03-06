import { Component, OnInit } from '@angular/core';
import {BooksLibraryService} from '../services/books-library.service';
import {AuthService} from '../services/auth.service';
import {Book} from '../models/book';
import {FormArray, FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit {
  isAuthorized: boolean = this.authServ.isAuthenticated();
  addingBook: boolean;
  bookForm: FormGroup;
  authForm: FormGroup;
  books: Book[];
  constructor(
    private bookServ: BooksLibraryService,
    private authServ: AuthService) {
  }
  ngOnInit(): void {
    this.bookServ.getBooks().subscribe(
      res => this.books = res,
      error => console.log(error)
    );
    this.bookForm = new FormGroup({
      title: new FormControl('', Validators.required),
      author: new FormControl('', Validators.required),
      year: new FormControl(''),
      gener: new FormControl('')
    });
    this.authForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    });
  }

  addBook(): void{
    this.addingBook = true;
    if (!this.isAuthorized)
      window.scrollTo(0, 0);
  }

  submit(): void{
    if (!this.isAuthorized)
    {
      alert('Необходимо авторизоваться');
      return;
    }
    this.addingBook = false;
    this.bookForm.value.yer += '01-01T00:00:00-00:00'; // Модифицировал дату, чтобы сервер принял
    this.bookServ.AddBook(this.bookForm.value).subscribe(
      n => console.log('Успешно добавлено'),
      error => console.log('Ошибка', error)
    );
    this.bookServ.getBooks().subscribe(
      res => this.books = res,
      error => console.log(error)
    );
  }

  authSubmit(): void{
    console.log(this.authForm.value);
    this.authServ.login(this.authForm.value.username, this.authForm.value.password).subscribe(
      ok => {console.log(this.isAuthorized = this.authServ.isAuthenticated()); },
      error => console.log('Error', error)
    );
  }

}
