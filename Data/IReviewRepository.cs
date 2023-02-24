using Library.Entities;

namespace Library.Data
{
    public interface IReviewRepository
    {
        Task<Review> GetById(int id);
        Task<IEnumerable<Review>> GetAll();
        Task Add(Review review);
        Task Update(Review review);
        Task Delete(int id);
    }
}
