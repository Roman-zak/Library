using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly LibraryDbContext _dbContext;

        public ReviewRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Review> GetById(int id)
        {
            return await _dbContext.Reviews.FindAsync(id);
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _dbContext.Reviews.ToListAsync();
        }

        public async Task Add(Review review)
        {
            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Review review)
        {
            _dbContext.Entry(review).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var review = await GetById(id);
            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();
        }
    }
}
