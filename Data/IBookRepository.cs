using Library.Entities;

namespace Library.Data
{
    public interface IBookRepository
    {
        Task<Book?> GetById(int id);
        Task<IEnumerable<Book>> GetAll();
        Task Add(Book book);
        Task Update(Book book);
        Task Delete(int id);
        public Task<bool> BookExists(int id);
    }
}
