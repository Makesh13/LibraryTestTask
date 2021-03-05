using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace Common
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder model)
        {
            SHA256 mySha256 = SHA256.Create();
            base.OnModelCreating(model);

            model.Entity<Book>().HasData(new Book
            {
                Id = Guid.Parse("e83b4bec-765b-4e13-a349-611ad5cda0cb"),
                Gener = "Юмористическое фэнтези",
                Title = "Мор, ученик Смерти",
                Year = new DateTime(1987, 01, 01)
            });
            model.Entity<Account>().HasData(new Account
            {
                Id = Guid.Parse("cdee6b90-d530-4e77-8980-5a845a88806a"),
                PasswordHash = string.Concat(mySha256.ComputeHash(Encoding.UTF8.GetBytes("mypassword")).Select(x => x.ToString("X2"))),
                UserName = "Makesh",
                RoleId = 2,
            });
            model.Entity<Account>().HasData(new Account
            {
                Id = Guid.Parse("c09c1ed7-ab92-46be-b5a9-d11741508b0c"),
                PasswordHash = string.Concat(mySha256.ComputeHash(Encoding.UTF8.GetBytes("mypassword11")).Select(x=>x.ToString("X2"))),
                UserName = "Makesh1",
                RoleId = 1,
            });
            model.Entity<Role>().HasData(new Role
            {
                Id = 1,
                Name = "user"
            });
            model.Entity<Role>().HasData(new Role
            {
                Id = 2,
                Name = "Admin"
            });
        }
        
    }

}
