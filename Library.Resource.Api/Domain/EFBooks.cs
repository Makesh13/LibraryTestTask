using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Library.Resource.Api.Domain
{
    //Класс прослойка между контекстом и контроллером
    public class EFBooks
    {
        private ApplicationContext DbContext { get; }
        public EFBooks(ApplicationContext context)
        {
            DbContext = context;
        }

        public IEnumerable<Book> GetBooks()
        {
            return DbContext.Books;
        }

        public Book GetBook(Guid id)
        {
            return DbContext.Books.FirstOrDefault(x => x.Id == id);
        }

        public void SaveBook(Book book)
        {
            if(book.Id == default)
            {
                DbContext.Entry(book).State = EntityState.Added;
            }
            else
            {
                DbContext.Entry(book).State = EntityState.Modified;
            }
            DbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            DbContext.Books.Remove(new Book() { Id = id });
            DbContext.SaveChanges();
        }
    }
}
