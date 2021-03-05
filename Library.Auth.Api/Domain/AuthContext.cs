using Library.Auth.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Library.Auth.Api.Domain
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; private set; }
        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Account>().HasData(new Account
            {
                Id = Guid.Parse(""),
                PasswordHash = new PasswordHasher<Account>().HashPassword(null, "mypassword"),
                UserName = "Makesh",
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
