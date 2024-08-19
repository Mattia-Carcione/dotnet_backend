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

using DTOs.BookDTOs;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/books")]
public class BookController : ControllerBase
{
    private readonly IExtendedRepository<Book> _repository;

    public BookController(IExtendedRepository<Book> repository)
    {
        _repository = repository;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookDTO book)
    {
        //automapper
        // await _repository.AddAsync(book);
        await _repository.SaveChangesAsync();

        return CreatedAtRoute("GetAsync", book);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateBookAsync([FromBody] CreateBookDTO book)
    {
        //automapper
        // _repository.Update(book);
        await _repository.SaveChangesAsync();

        return CreatedAtRoute("GetAsync", book);
    }

    [HttpGet(Name = "GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        var books = await _repository.GetAllAsync();

        //automapper

        return Ok(books);
    }

    [HttpGet("{id}", Name = "GetAsync")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        var book = await _repository.GetAsync(id);

        if (book == null)
            return NotFound();

        //automapper

        return Ok(book);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchByCriteriaAsync([FromQuery] string value)
    {
        var books = await _repository.SearchByCriteriaAsync(b => b.Title.Contains(value) || (b.Author != null && b.Author.LastName.Contains(value)));

        //automapper

        return Ok(books);
    }
}
