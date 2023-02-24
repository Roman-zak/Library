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
using Library.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace Library.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService BookService;
        private readonly IConfiguration Configuration;
        private readonly IValidator<BookSaveDto> BookSaveDtoValidator;
        private readonly IValidator<ReviewSaveDto> ReviewSaveDtoValidator;
        private readonly IValidator<RatingSaveDto> RatingSaveDtoValidator;

        public BooksController(IBookService bookService, 
                                IConfiguration configuration,
                                IValidator<BookSaveDto> bookSaveDtoValidator,
                                IValidator<ReviewSaveDto> reviewSaveDtoValidator,
                                IValidator<RatingSaveDto> ratingSaveDtoValidator)
        {
            BookService = bookService;
            Configuration = configuration;
            BookSaveDtoValidator = bookSaveDtoValidator;
            ReviewSaveDtoValidator = reviewSaveDtoValidator;
            RatingSaveDtoValidator = ratingSaveDtoValidator;
        }

        // GET: api/Books?order=author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookOverviewDto>>> GetOrderedBooksOverview([FromQuery]string order)
        {
            IEnumerable<BookOverviewDto> bookOverviewDtos = await BookService.GetOrderedBooksOverview(order);
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
            BookDetalizedDto bookDetalizedDto = await BookService.GetById(id);
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
            if (!await BookService.BookExists(id))
            {
                return NotFound();
            }
            BookService.Delete(id);

            return NoContent();
        }

        // POST: api/books/save
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("save")]
        public async Task<ActionResult<IdResponceDto>> PostBook(BookSaveDto bookSaveDto)
        {
            ValidationResult result = await BookSaveDtoValidator.ValidateAsync(bookSaveDto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var bookIdDto = await BookService.SaveOrUpdate(bookSaveDto);
            
            return Ok(bookIdDto);
        }
        [HttpPut]
        [Route("{id}/review")]
        public async Task<ActionResult<IdResponceDto?>> ReviewBook(int id,ReviewSaveDto reviewSaveDto)
        {
            ValidationResult result = await ReviewSaveDtoValidator.ValidateAsync(reviewSaveDto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var reviewIdDto = await BookService.AddReview(id, reviewSaveDto);
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
            ValidationResult result = await RatingSaveDtoValidator.ValidateAsync(rateSaveDto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var rateIdDto = await BookService.AddRating(id, rateSaveDto);
            if (rateIdDto == null)
            {
                return NotFound();
            }
            return Ok(rateIdDto);
        }
    }
}
