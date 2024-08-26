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

    public BookController(IExtendedRepository<Book> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookDTO book)
    {
        var mappedBook = _mapper.Map<Book>(book);

        await _repository.AddAsync(mappedBook);
        await _repository.SaveChangesAsync();

        return CreatedAtRoute("GetAsync", mappedBook);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateBookAsync([FromBody] CreateBookDTO book)
    {
        var mappedBook = _mapper.Map<Book>(book);

        _repository.Update(mappedBook);
        await _repository.SaveChangesAsync();

        return CreatedAtRoute("GetAsync", mappedBook);
    }

    [HttpGet(Name = "GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        var books = await _repository.GetAllAsync();

        var mappedBooks = _mapper.Map<IEnumerable<BookDTO>>(books);

        return Ok(mappedBooks);
    }

    [HttpGet("{id}", Name = "GetAsync")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        var book = await _repository.GetAsync(id);

        if (book == null)
            return NotFound();

        var mappedBook = _mapper.Map<BookDTO>(book);

        return Ok(mappedBook);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchByCriteriaAsync([FromQuery] string value)
    {
        var books = await _repository.SearchByCriteriaAsync(b =>
            b.Title.Contains(value) || (b.Author != null && b.Author.LastName.Contains(value))
        );

        var mappedBooks = _mapper.Map<IEnumerable<BookDTO>>(books);

        return Ok(mappedBooks);
    }
}
