using Library.Dtos;
using Library.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Library.Controllers
{
    [Route("api/recommended")]
    [ApiController]
    public class RecomendedController : ControllerBase
    {
        IBookService bookService;
        public RecomendedController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookOverviewDto>>> GetBooks([FromQuery] string genre)
        {
            IEnumerable<BookOverviewDto> bookOverviewDtos = await bookService.GetRecommendedBooksByGenre(genre);
            if(bookOverviewDtos == null)
            {
                return NotFound();
            }
            return Ok(bookOverviewDtos);
        }
    }
}
