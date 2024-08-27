/*
*TODO:
*Aggiungere alla solution una web API che espone
- Ricerca per titolo
- Ricerca per libro
- ricerca tutti i libri
- Ricerca per autore
- Salva Libro
- Aggiorna Libro
*/

using AutoMapper;
using DTOs.BookDTOs;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/books")]
public class BookController : ControllerBase
{
    private readonly IExtendedRepository<Book> _repository;
    private readonly IMapper _mapper;
    private const int MaxPageSize = 25;

    public BookController(IExtendedRepository<Book> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    private async Task<IEnumerable<BookDTO>> GetAllAsync(int pageNumber, int pageSize)
    {
        var books = await _repository.GetAllAsync(pageNumber, pageSize, include: q => q.OrderBy(b=> b.Title));

        var mappedBooks = _mapper.Map<IEnumerable<BookDTO>>(books);

        return mappedBooks;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookDTO book)
    {
        var mappedBook = _mapper.Map<Book>(book);

        await _repository.AddAsync(mappedBook);
        await _repository.SaveChangesAsync();

        return CreatedAtRoute("GetAsync", new { id = mappedBook.Id }, mappedBook);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateBookAsync([FromRoute] int id,
        [FromBody] UpdateBookDTO book)
    {
        var existingBook = await _repository.GetAsync(id);

        if (existingBook == null)
            return NotFound();

        _mapper.Map(book, existingBook);

        _repository.Update(existingBook);
        await _repository.SaveChangesAsync();

        return NoContent();
    }


    [HttpGet("{id}", Name = "GetAsync")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        var book = await _repository.GetAsync(id, include: query =>
                query.Include(b => b.Author)
                    .Include(b => b.Editor)
                    .Include(b => b.Categories)
        );

        if (book == null)
            return NotFound();

        var mappedBook = _mapper.Map<BookDetailDTO>(book);

        return Ok(mappedBook);
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllAsync([FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery(Name = "author")] string? author = null,
        [FromQuery(Name = "title")] string? title = null)
    {
        if(pageSize > MaxPageSize)
            pageSize = MaxPageSize;

        if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(author))
            return Ok(await GetAllAsync(pageNumber, pageSize));

        title = title?.Trim();
        author = author?.Trim();

        var books = await _repository.SearchByCriteriaAsync(pageNumber, pageSize, b =>
            (string.IsNullOrEmpty(title) || b.Title.Contains(title)) && 
            (string.IsNullOrEmpty(author) || (b.Author != null && b.Author.LastName.Contains(author))),
            b => b.OrderBy(b => b.Title)
        );

        var mappedBooks = _mapper.Map<IEnumerable<BookDTO>>(books);

        return Ok(mappedBooks);
    }
}
