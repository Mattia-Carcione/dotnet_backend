/*
*TODO:
*Aggiungere alla solution una web API che espone 
- Ricerca per titolo
- Ricerca per autore
- Salva Libro
- Aggiorna Libro
*/

using DTOs.BookDTOs;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace WebApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IExtendedRepository<Book> _repository;

    public BookController(IExtendedRepository<Book> repository)
    {
        _repository = repository;
    }

    [HttpPost("/create")]
    public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookDTO book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //automapper

        // await _repository.AddAsync(book);
        await _repository.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("/update")]
    public async Task<IActionResult> UpdateBookAsync([FromBody] CreateBookDTO book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //automapper

        // _repository.Update(book);
        await _repository.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("/book/{param}")]
    public async Task<IActionResult> SearchByCriteriaAsync([FromRoute] string param)
    {
        var books = await _repository.SearchByCriteriaAsync(b => b.Title.Contains(param) || (b.Author != null && b.Author.LastName.Contains(param)));

        //automapper
        
        return Ok(books);
    }
}
