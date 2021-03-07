import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {Book} from '../models/book';

import {AuthService} from '../services/auth.service';
import {BooksLibraryService} from '../services/books-library.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
  editForm: FormGroup;
  booksArr: Book[];
  book: Book;
  isAuthorized: boolean;
  constructor(private activatedRoute: ActivatedRoute,
              private router: Router,
              private authServ: AuthService,
              private libraryServ: BooksLibraryService) { }

  // В идеале нужно вынести работу с масивом books в отдельный сервис
  ngOnInit(): void {

    this.isAuthorized = this.authServ.isAuthenticated();
    this.editForm = new FormGroup({
      title: new FormControl('', Validators.required),
      author: new FormControl('', Validators.required),
      year: new FormControl(''),
      gener: new FormControl('')
    });
    // Сначала вытаскиваем данные в books, а потом ищем там элемент с айди из роута
    this.libraryServ.getBooks().subscribe(
      res => {
        this.booksArr = res;

        this.findBookById();
        this.editForm.controls.title.setValue(this.book.title);
        this.editForm.controls.author.setValue(this.book.author);
        this.editForm.controls.year.setValue(this.book.year.slice(0, 4));
        this.editForm.controls.gener.setValue(this.book.gener);
        },
      error => console.log(error)
    );
  }

  // Конечно лучше бы было реализовать метод поиска по айди на сервере...
  findBookById(): void
  {
    this.activatedRoute.params.subscribe((params: Params) => {
      console.log(params);
      this.book = this.booksArr.find(value => value.id === params.id);
    });
  }
  // Так как редактирование происходит в том же контроллере, что и добавление
  // Просто кидаем объект book по тому же пути, только здесь у него есть еще и айди
  edit(): void
  {
    const editedBook: Book = {
      id: this.book.id,
      title: this.editForm.value.title,
      author: this.editForm.value.author,
      gener: this.editForm.value.gener,
      year: this.editForm.value.year + '-01-01T00:00:00' // Модифицировал дату, чтобы сервер принял
  };
    this.libraryServ.addBook(editedBook).subscribe(
      ok => this.router.navigate(['']),
      error => console.log(error)
    );
  }


}
