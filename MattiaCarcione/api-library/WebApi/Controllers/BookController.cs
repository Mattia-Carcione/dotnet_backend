using Microsoft.AspNetCore.Mvc;
using LibraryModel.Model;
using LibraryInterface.Interfaces;
using AutoMapper;
using LibraryRepository.Repositories;
using LibraryDtos.Dtos.Book;
using Azure;
using LibraryDtos.Dtos.Author;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _bookRepository.GetAllAsync();

            var books = response.Select(book => _mapper.Map<BookDto>(book)).ToList();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var response = await _bookRepository.GetByIdAsync(id);

            if (response == null) return NotFound();

            var book =  _mapper.Map<BookDetailDto>(response);

            return Ok(book);
        }

        [HttpGet("booking={id}")]
        public async Task<IActionResult> GetBookByBooking([FromRoute] int id)
        {
            var response = await _bookRepository.GetBookByBookingAsync(id);

            if (response == null) return NotFound();

            var book = _mapper.Map<BookDto>(response);

            return Ok(book);
        }

        [HttpGet("{id}/user={user}")]
        public async Task<IActionResult> GetIsBookReturned([FromRoute] int id, [FromRoute] string user)
        {
            var response = await _bookRepository.GetBookIsReturnedAsync(id, user);

            return Ok(response);
        }

        [HttpGet("{id}/sold-out")]
        public async Task<IActionResult> GetIsBookSoldOut([FromRoute] int id)
        {
            var response = await _bookRepository.GetBookIsSoldOutAsync(id);

            return Ok(response);
        }

        [HttpGet("author={lastName}")]
        public async Task<IActionResult> GetSearchBooksByAuthor([FromRoute] string lastName)
        {
            var response = await _bookRepository.GetBooksByAuthorAsync(lastName);

            var books = response.Select(book => _mapper.Map<BookDto>(book)).ToList();

            return Ok(books);
        }

        [HttpGet("category={category}")]
        public async Task<IActionResult> GetSearchBooksByCategories([FromRoute] string category)
        {
            var response = await _bookRepository.GetBooksByCategoryAsync(category);

            var books = response.Select(book => _mapper.Map<BookDto>(book)).ToList();

            return Ok(books);
        }

        [HttpGet("pageOffset={pageOffset}-pageLimit={pageLimit}")]
        public async Task<IActionResult> GetSearchBooksByNumberOfPages([FromRoute] int pageOffset, [FromRoute] int pageLimit)
        {
            var response = await _bookRepository.GetBooksByNumbOfPagesAsync(pageOffset, pageLimit);

            var books = response.Select(book => _mapper.Map<BookDto>(book)).ToList();

            return Ok(books);
        }

        [HttpGet("dateOffset={dateOffset}-DateLimit{dateLimit}")]
        public async Task<IActionResult> GetSearchBooksByPublishingDate([FromRoute] DateTime dateOffset, [FromRoute] DateTime dateLimit)
        {
            var response = await _bookRepository.GetBooksByPublishingDateAsync(dateOffset, dateLimit);

            var books = response.Select(book => _mapper.Map<BookDto>(book)).ToList();

            return Ok(books);
        }

        [HttpGet("title={title}")]
        public async Task<IActionResult> GetSearchBooksByTitle([FromRoute] string title)
        {
            var response = await _bookRepository.GetBooksByTitleAsync(title);

            var books = response.Select(book => _mapper.Map<BookDto>(book)).ToList();

            return Ok(books);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNewBook([FromBody] BookToCreateDto book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var bookMapped = _mapper.Map<Book>(book);

            var response = await _bookRepository.CreateAsync(bookMapped);

            var responseMapped = _mapper.Map<BookDetailDto>(response);

            return Ok(responseMapped);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookDto book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var bookMapped = _mapper.Map<Book>(book);

            var response = await _bookRepository.UpdateAsync(id, bookMapped);

            var responseMapped = _mapper.Map<BookDto>(response);

            return Ok(responseMapped);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var response = await _bookRepository.DeleteAsync(id);

            return Ok(response);
        }
    }
}
