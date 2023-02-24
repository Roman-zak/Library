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
        public async Task<ActionResult<IdResponceDto>> PostBook(BookSaveDto bookSaveDto)
        {
            // save the book to the repository
            var bookIdDto = await bookService.SaveOrUpdate(bookSaveDto);
            
            return Ok(bookIdDto);
        }
        [HttpPut]
        [Route("{id}/review")]
        public async Task<ActionResult<IdResponceDto?>> ReviewBook(int id,ReviewSaveDto reviewSaveDto)
        {
            // save the book to the repository
            var reviewIdDto = await bookService.AddReview(id, reviewSaveDto);
            if(reviewIdDto == null)
            {
                return NotFound();
            }
            return Ok(reviewIdDto);
        }
        [HttpPut]
        [Route("{id}/rate")]
        public async Task<ActionResult<IdResponceDto?>> RateBook(int id, RatingSaveDto rateSaveDto)
        {
            // save the book to the repository
            var rateIdDto = await bookService.AddRating(id, rateSaveDto);
            if (rateIdDto == null)
            {
                return NotFound();
            }
            return Ok(rateIdDto);
        }
    }
}
