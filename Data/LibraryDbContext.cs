using Library.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Library.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
       
        }
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "LibraryDb");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
/*            builder.Entity<Book>(a =>
            {
                a.HasData(
                new Book { Id = 1, Title = "The Great Gatsby", Cover = "cover", Content = "content", Author = "F. Scott Fitzgerald", Genre = "Classic" }
                );
            });*/
            //  new DbInitializer(builder).Seed();
        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
