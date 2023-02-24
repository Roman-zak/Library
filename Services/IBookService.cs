using Library.Dtos;

namespace Library.Services
{
    public interface IBookService
    {
        Task<IdResponceDto?> AddRating(int id, RatingSaveDto rateSaveDto);
        Task<IdResponceDto?> AddReview(int id, ReviewSaveDto reviewSaveDto);
        Task<bool> BookExists(int id);
        void Delete(int id);
        Task<BookDetalizedDto> GetById(int id);
        public Task<IEnumerable<BookOverviewDto>> GetOrderedBooksOverview(string orderValue);
        Task<IEnumerable<BookOverviewDto>> GetRecommendedBooksByGenre(string genre);
        Task<IdResponceDto> SaveOrUpdate(BookSaveDto bookSaveDto);
    }
}
