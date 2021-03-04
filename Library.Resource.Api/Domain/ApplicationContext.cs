using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Resource.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Resource.Api.Domain
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {

        }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(new Book
            {
                Id = Guid.Parse("e83b4bec-765b-4e13-a349-611ad5cda0cb"), Gener = "Юмористическое фэнтези",
                Title = "Мор, ученик Смерти", Year = new DateTime(1987, 01, 01)
            });
        }
    }
}
