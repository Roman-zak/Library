using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Entities;
using AutoMapper;
using Library.Data;
using Library.Dtos;
using Library.Services;

namespace Library.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;
        private readonly IConfiguration Configuration;

        public BooksController(IBookService bookService, IConfiguration configuration)
        {
            this.bookService = bookService;
            this.Configuration = configuration;
        }
        // GET: api/Books?order=author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookOverviewDto>>> GetOrderedBooksOverview([FromQuery]string order)
        {
            IEnumerable<BookOverviewDto> bookOverviewDtos = await bookService.GetOrderedBooksOverview(order);
            if(bookOverviewDtos == null)
            {
                return NotFound();
            }
            return  Ok(bookOverviewDtos);
        }
        // GET: api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetalizedDto>> GetBook(int id)
        {
            BookDetalizedDto bookDetalizedDto = await bookService.GetById(id);
            if (bookDetalizedDto == null)
            {
                return NotFound();
            }
            return Ok(bookDetalizedDto);
        }


        // DELETE: api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id, [FromQuery] string secret)
        {
            if (secret != Configuration["secretKey"])
            {
                return Forbid();
            }
            if (!await bookService.BookExists(id))
            {
                return NotFound();
            }
            bookService.Delete(id);

            return NoContent();
        }

        // POST: api/books/save
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("save")]
        public async Task<ActionResult<BookDto>> PostBook(BookSaveDto bookSaveDto)
        {
            // save the book to the repository
            var bookIdDto = await bookService.SaveOrUpdate(bookSaveDto);
            
            return Ok(bookIdDto);
        }
        /*        [HttpGet("{value}")]
                public async Task<ActionResult<IEnumerable<BookOverviewDto>>> GetOrderedBooksOverview(string value)
                {
                    return Ok(await bookService.GetOrderedBooksOverview(value));
                }*/

        /*        // GET: api/Books
                [HttpGet]
                public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
                {
                    IEnumerable<Book> books = await _bookRepository.GetAll();
                    return books.Select(b => _mapper.Map<BookDto>(b)).ToList();
                }

                // GET: api/Books/5
                [HttpGet("{id}")]
                public async Task<ActionResult<BookDto>> GetBook(int id)
                {
                     var book = await _bookRepository.GetById(id);

                    if (book == null)
                    {
                        return NotFound();
                    }

                    return _mapper.Map<BookDto>(book);
                }

                // PUT: api/Books
                // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                [HttpPut]
                public async Task<IActionResult> PutBook(BookDto bookDto)
                {
                    Book book = _mapper.Map<Book>(bookDto);
                    await _bookRepository.Update(book);

                    return NoContent();
                }

                // POST: api/Books
                // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                [HttpPost]
                public async Task<ActionResult<BookDto>> PostBook(BookDto bookDto)
                {
                    Book book = _mapper.Map<Book>(bookDto);
                    await _bookRepository.Add(book);

                    return CreatedAtAction("GetBook", new { id = book.Id }, book);
                }

                // DELETE: api/Books/5
                [HttpDelete("{id}")]
                public async Task<IActionResult> DeleteBook(int id)
                {
                    if (!_bookRepository.BookExists(id))
                    {
                        return NotFound();
                    }
                    await _bookRepository.Delete(id);

                    return NoContent();
                }*/
    }
}
