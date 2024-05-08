using Azure.Core;
using Dtos.Book;
using Dtos.BookDtos;

using Entities.Data;
using Intefaces.crud.entities;
using Mapper.Books;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly Library_DbContext _context;
        private readonly IBookRepository _bookRepo;
        public BookController(Library_DbContext context, IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookRepo.GetAllAsync();
            var bookDto = books.Select(s => s.ToBookDto());
            return Ok(bookDto);
        }

        [HttpGet("by-title/{Title}")]
        public async Task<IActionResult> GetByTitle([FromRoute] string Title)
        {
            var book = await _bookRepo.GetByTitleAsync(Title);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book.ToBookDto());
        }


        [HttpGet("by-Author/")]
        public async Task<IActionResult> GetByAuthor([FromQuery] string name, [FromQuery] string surname)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname))
            {
                return BadRequest("Name and surname are required parameters.");
            }

            var books = await _bookRepo.GetByAuthorAsync(name, surname);
            if (books == null || !books.Any())
            {
                return NotFound("No books found for the provided author.");
            }
            return Ok(books.Select(book => book.ToBookDto()));
        }






        [HttpGet("by-id/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            var book = await _bookRepo.GetByIdAsync(Id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book.ToBookDto());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookRequestDto bookDto)
        {
            var bookModel = bookDto.ToBookFromCreateBookDto();
            await _bookRepo.CreateAsync(bookModel);
            return CreatedAtAction(nameof(GetById), new { id = bookModel.BookId }, bookModel.ToBookDto());
        }



        [HttpPut]
        [Route("Update-{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookRequestDto updateDto)
        {
            var bookModel = await _bookRepo.UpdateAsync(id, updateDto);

            if (bookModel == null)
            {
                return NotFound();
            }


            return Ok(bookModel.ToBookDto());

        }

        [HttpDelete]
        [Route("delete-{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var bookModel = await _bookRepo.DeleteAsync(id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
