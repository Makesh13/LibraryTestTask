using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Library.Resource.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Library.Resource.Api.Domain
{
    public class EFBooks
    {
        private ApplicationContext DbContext { get; }
        public EFBooks(ApplicationContext context)
        {
            this.DbContext = context;
        }

        public IEnumerable<Book> GetBooks()
        {
            return this.DbContext.Books;
        }

        public Book GetBook(Guid id)
        {
            return this.DbContext.Books.FirstOrDefault(x => x.Id == id);
        }

        public void SaveBook(Book book)
        {
            if(book.Id == default)
            {
                this.DbContext.Entry(book).State = EntityState.Added;
            }
            else
            {
                this.DbContext.Entry(book).State = EntityState.Modified;
            }
            DbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            this.DbContext.Books.Remove(new Book() { Id = id });
            DbContext.SaveChanges();
        }
    }
}
