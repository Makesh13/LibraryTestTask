import { Component, OnInit } from '@angular/core';
import {BooksLibraryService} from '../services/books-library.service';
import {AuthService} from '../services/auth.service';
import {Book} from '../models/book';
import {FormArray, FormControl, FormGroup, Validators} from '@angular/forms';
import set = Reflect.set;
import {Router} from '@angular/router';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit {
  isAuthorized: boolean;
  addingBook: boolean;
  bookForm: FormGroup;
  authForm: FormGroup;
  books: Book[];
  constructor(
    private bookServ: BooksLibraryService,
    private authServ: AuthService,
    private router: Router,
    ) {}
  ngOnInit(): void {
    this.isAuthorized = this.authServ.isAuthenticated();
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
    let newBook: Book = {
      title: this.bookForm.value.title,
      year: this.bookForm.value.year + '-01-01T00:00:00', // Модифицировал дату, чтобы сервер принял
      gener: this.bookForm.value.gener,
      author: this.bookForm.value.author
    };
    if (!this.isAuthorized)
    {
      alert('Необходимо авторизоваться');
      return;
    }
    this.addingBook = false;
    console.log(newBook);
    this.bookServ.addBook(newBook).subscribe(
      n => {console.log('Успешно добавлено'); this.ngOnInit(); },
      error => console.log('Ошибка', error)
    );
  }

  authSubmit(): void{
    console.log(this.authForm.value);
    this.authServ.login(this.authForm.value.username, this.authForm.value.password).subscribe(
      ok => {console.log(this.isAuthorized = this.authServ.isAuthenticated()); },
      error => console.log('Error', error)
    );
  }

  editBook(): void{
    console.log('hello');
    // this.router.navigate(['edit', id]);
  }

  deleteBook(id: string): void{
    this.bookServ.deleteBook(id).subscribe(
      res => {console.log('Успешное удаление'); this.ngOnInit(); },
      error => console.log(error)
    );
  }

  logout(): void{
    this.authServ.logout();
    this.ngOnInit();
  }

}
