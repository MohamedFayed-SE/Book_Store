using BAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder moduleBuilder)
        {
            moduleBuilder.Entity<Author>()
            .HasMany(A => A.Books)
            .WithMany(B => B.Authors)
            .UsingEntity<AuthorBook>(
              j=>j
                 .HasOne(ab=>ab.Book)
                 .WithMany(a=>a.AuthorBooks)
                 .HasForeignKey(ab=>ab.BookId),
              j=>j 
                 .HasOne(ab=>ab.Author)
                 .WithMany(a=>a.AuthorBooks)
                 .HasForeignKey(ab=>ab.AuthorId),
              j =>
              {
                  j.HasKey(c => new { c.AuthorId, c.BookId });
              }
               
                
                
                );
            base.OnModelCreating(moduleBuilder);
        }

       public DbSet<Book> books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }


    }
}
