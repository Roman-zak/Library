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
        public async static void SeedData(LibraryDbContext context)
        {
            var genres = new[]
            {
                    "Poetry",
                    "Detective",
                    "Thriller",
                    "Nonfiction"
                };
  
            var random = new Random();
            var coverPlaceholder = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBxMTEhUSEBMVFxUXFxUVFxgVFRcYFRcXGBYXGBcYFhcYHSggGBolGxUVITEhJSkrLi4uFx8zODMtNygtLisBCgoKDg0OGhAQGy0lICUtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLf/AABEIAOEA4QMBEQACEQEDEQH...";

            var books = new List<Book>();
            for (int i = 0; i < 10; i++)
            {
                var book = new Book
                {
                    Title = $"Book {i + 1}",
                    Author = "Unknown",
                    Genre = genres[random.Next(genres.Length)],
                    Cover = coverPlaceholder,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec euismod ipsum vitae risus fringilla gravida. Pellentesque faucibus metus vel rutrum laoreet. Sed tincidunt felis nec nisl bibendum aliquet. Nulla tincidunt lectus eget libero aliquet, non tristique orci egestas. Fusce gravida sapien magna, nec maximus odio mollis sit amet. Vivamus elementum ut dolor sit amet cursus. Integer ut arcu et nulla maximus placerat. Maecenas vestibulum, odio vel suscipit venenatis, sapien ante faucibus eros, eget ultrices velit magna vitae turpis. Duis at volutpat ipsum. Donec viverra sem sed magna lacinia, eu gravida ipsum malesuada. Praesent blandit velit at aliquet volutpat. Nulla facilisi. Phasellus vulputate dolor quis tristique maximus.",
                    Ratings = new List<Rating>(),
                    Reviews = new List<Review>()
                };

                // Generate random number of ratings (0-5)
                var numRatings = random.Next(6);
                for (int j = 0; j < numRatings; j++)
                {
                    book.Ratings.Add(new Rating { Score = random.Next(1, 6) });
                }

                // Generate random number of reviews (8-15)
                var numReviews = random.Next(8, 16);
                for (int j = 0; j < numReviews; j++)
                {
                    book.Reviews.Add(new Review
                    {
                        Message = $"Review {j + 1} for Book {i + 1}",
                        Reviewer = $"Reviewer {j + 1}"
                    });
                }
                books.Add(book);
            }
            await context.Books.AddRangeAsync(books);
            context.SaveChanges();
        }
    }
}
