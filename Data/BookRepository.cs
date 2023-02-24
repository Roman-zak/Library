using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book?> GetById(int id)
        {
            return await _dbContext.Books
                .Include(b => b.Reviews)
                .Include(b => b.Ratings)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _dbContext.Books
                .Include(b => b.Reviews)
                .Include(b => b.Ratings)
                .ToListAsync();
        }

        public async Task Add(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Book book)
        {
            _dbContext.Entry(book).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var book = await GetById(id);
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> BookExists(int id)
        {
            return await _dbContext.Books.AnyAsync(e => e.Id == id);
        }
     //   public async void AddReview(int )
    }
}
