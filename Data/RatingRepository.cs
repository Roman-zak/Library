using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class RatingRepository : IRatingRepository
    {
        private readonly LibraryDbContext _dbContext;

        public RatingRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Rating> GetById(int id)
        {
            return await _dbContext.Ratings.FindAsync(id);
        }

        public async Task<IEnumerable<Rating>> GetAll()
        {
            return await _dbContext.Ratings.ToListAsync();
        }

        public async Task Add(Rating rating)
        {
            await _dbContext.Ratings.AddAsync(rating);
            
            Book book = await _dbContext.Books.FindAsync(rating.Book.Id);
            if (book == null)
            {
                book = rating.Book;
                _dbContext.Books.Add(book);
            }
            if (book.Ratings == null)
            {
                book.Ratings = new List<Rating>();
            }
            book.Ratings.Add(rating);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Rating rating)
        {
            _dbContext.Entry(rating).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var rating = await GetById(id);
            _dbContext.Ratings.Remove(rating);
            await _dbContext.SaveChangesAsync();
        }
        public bool RatingExists(int id)
        {
            return _dbContext.Ratings.Any(e => e.Id == id);
        }
    }
}
