using Library.Entities;

namespace Library.Data
{
    public interface IRatingRepository
    {
        Task<Rating> GetById(int id);
        Task<IEnumerable<Rating>> GetAll();
        Task Add(Rating rating);
        Task Update(Rating rating);
        Task Delete(int id);
        public bool RatingExists(int id);
    }
}
