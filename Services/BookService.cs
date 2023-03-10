using AutoMapper;
using Library.Data;
using Library.Dtos;
using Library.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Library.Services
{
    public class BookService : IBookService
    {
        IBookRepository bookRepository;
        IReviewRepository reviewRepository;
        private readonly IMapper mapper;

        public BookService(IBookRepository bookRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<BookOverviewDto>> GetOrderedBooksOverview(string orderValue)
        {
            IEnumerable<Book> books = await bookRepository.GetAll().ConfigureAwait(false);
            if(orderValue == "author")
            {
                return books.Select(book => mapper.Map<BookOverviewDto>(book)).OrderBy(bookDto=>bookDto.Author);
            }
            else
            {
                return books.Select(book => mapper.Map<BookOverviewDto>(book)).OrderBy(bookDto => bookDto.Title);
            }
        }

        public async Task<IEnumerable<BookOverviewDto>> GetRecommendedBooksByGenre(string genre)
        {
            IEnumerable<Book> books = await bookRepository.GetAll().ConfigureAwait(false);
            return books
                .Where(book => book.Genre == genre && (book.Reviews!=null && book.Reviews.Any() && book.Reviews.Count > 10))
                .Select(book => mapper.Map<BookOverviewDto>(book))
                .OrderBy(bookDto => bookDto.Rating)
                .Take(10);
        }

        public async Task<BookDetalizedDto> GetById(int id)
        {
            Book book = await bookRepository.GetById(id).ConfigureAwait(false);
            return mapper.Map<BookDetalizedDto>(book);
        }

        public async Task<bool> BookExists(int id)
        {
            return await bookRepository.BookExists(id);
        }

        public async void Delete(int id)
        {
            await bookRepository.Delete(id);
        }

        public async Task<IdResponceDto> SaveOrUpdate(BookSaveDto bookSaveDto)
        {
            Book book = mapper.Map<Book>(bookSaveDto);
            if (bookSaveDto.Id.HasValue)
            {
                bool bookExists = await bookRepository.BookExists(book.Id);
                if (bookExists)
                {
                    await bookRepository.Update(book);
                }
                else
                {
                    await bookRepository.Add(book);
                }
            }
            else
            {
                await bookRepository.Add(book);
            }

            return new IdResponceDto() { Id = book.Id};
        }

        public async Task<IdResponceDto?>  AddReview(int id, ReviewSaveDto reviewSaveDto)
        {
            Review review = mapper.Map<Review>(reviewSaveDto);
            Book? book = await bookRepository.GetById(id);
            if(book == null)
            {
                return null;
            }
            //review.Book = book;
            //await reviewRepository.Add(review);
            book.Reviews ??= new List<Review>();
            book.Reviews.Add(review);
            await bookRepository.Update(book);
            return new IdResponceDto { Id = review.Id };
        }

        public async Task<IdResponceDto?> AddRating(int id, RatingSaveDto rateSaveDto)
        {
            Rating rating = mapper.Map<Rating>(rateSaveDto);
            Book? book = await bookRepository.GetById(id);
            if (book == null)
            {
                return null;
            }
            book.Ratings ??= new List<Rating>();
            book.Ratings.Add(rating);
            await bookRepository.Update(book);
            return new IdResponceDto { Id = rating.Id };
        }
    }
}
