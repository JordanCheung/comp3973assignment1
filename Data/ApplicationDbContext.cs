using System;
using System.Collections.Generic;
using System.Text;
using Assignment1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            IdentityUser user = new IdentityUser
            {
                UserName = "a@a.a",
                NormalizedUserName = "A@A.A",
                Email = "a@a.a",
                NormalizedEmail = "A@A.A",
                EmailConfirmed = true
            };

            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            user.PasswordHash = ph.HashPassword(user, "P@$$w0rd");

            builder.Entity<IdentityUser>().HasData(user);
        }

        public DbSet<Book> Book { get; set; }
    }
}
