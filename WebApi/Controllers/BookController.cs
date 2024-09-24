using Application.Interfaces;
using Domain.Entities;
using Domain.EntitiesDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("addbooklist")]
        public async Task<IActionResult> AddBook(List<Book> books)
        {
            var result = await _bookService.AddBookList(books);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("setbookimage")]
        public async Task<IActionResult> SetBookImage(UpdateBookImageDTO updateBookImageDTO)
        {
            var result = await _bookService.SetBookImage(updateBookImageDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult UpdateBook(BookUpdateDTO bookUpdateDTO)
        {
            var result = _bookService.Update(bookUpdateDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var result = _bookService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetBook(int id)
        {
            var result = _bookService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAllBook()
        {
            var result = _bookService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("searchbook")]
        public async Task<IActionResult> Search(string term)
        {
            var result = await _bookService.Search(term,["BookName","AuthorName"]);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
