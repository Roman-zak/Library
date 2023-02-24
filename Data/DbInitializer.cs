using Library.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Library.Data
{
    public class DbInitializer
    {
        ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<Book>(a =>
            {
                a.HasData(
                new Book { Id = 1, Title = "The Great Gatsby", Cover = "cover", Content = "content", Author = "F. Scott Fitzgerald", Genre = "Classic" }
                );
            });
            modelBuilder.Entity<Book>(a =>
            {
                a.HasData(
            new Book { Id = 2, Title = "To Kill a Mockingbird", Cover = "cover", Content = "content", Author = "Harper Lee", Genre = "Classic" }

                );
            });
            modelBuilder.Entity<Book>(a =>
            {
                a.HasData(
                    new Book { Id = 3, Title = "1984", Cover = "cover", Content = "content", Author = "George Orwell", Genre = "Dystopian" });
            });
            modelBuilder.Entity<Book>(a =>
            {
                a.HasData(
            new Book { Id = 4, Title = "Pride and Prejudice", Cover = "cover", Content = "content", Author = "Jane Austen", Genre = "Classic" }        
                );
            });
        }
    }
}
