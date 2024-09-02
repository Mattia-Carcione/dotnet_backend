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

using Asp.Versioning;
using AutoMapper;
using DTOs.BookDTOs;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace WebApi.Controllers;

/// <summary>
/// Controller provides book-related operation.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/books")]
[ApiVersion(1)]
public class BookController : ControllerHelper<Book, BookDTO, BookDetailDTO>
{
    /// <summary>
    /// Initializes a new instance of <see cref="BookController"/>.
    /// </summary>
    /// 
    /// <param name="repository">The repository interface that provides the CRUD operation methods.</param>
    /// 
    /// <param name="mapper">A mapper object that maps entities to each other.</param>
    public BookController(IExtendedRepository<Book> repository, IMapper mapper) : base(mapper, repository)
    { }

    /// <summary>
    /// Creates a new instance of <see cref="Book"/>.
    /// </summary>
    /// 
    /// <param name="book">The object DTO for creating book.</param>
    /// 
    /// <returns>A task representing the asynchronous operation for creating a new book.</returns>
    /// 
    /// <response code="201">If the book was created correctly.</response>
    /// 
    /// <response code="400">If the data provided for the creation of the book is invalid.</response>
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookDTO book)
    {
        var mappedBook = _mapper.Map<Book>(book);

        await _repository.AddAsync(mappedBook);
        await _repository.SaveChangesAsync();

        return CreatedAtRoute("GetBookAsync", new { id = mappedBook.Id }, mappedBook);
    }

    /// <summary>
    /// Gets the item with the specified id.
    /// </summary>
    /// 
    /// <param name="id">The id of the entity.</param>
    /// 
    /// <returns>A task representing asynchronous operation that returns the <see cref="OkResult"/> with the object created; else, <see cref="NotFoundResult"/>.</returns>
    /// 
    /// <response code="200">If the book was successfully found.</response>
    /// 
    /// <response code="404">If the book doesn't exist in the current context.</response>
    [HttpGet("{id}", Name = "GetBookAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookAsync([FromRoute] int id)
    {
        var book = await GetAsync(id, query =>
                query.Include(b => b.Author)
                    .Include(b => b.Editor)
                    .Include(b => b.Categories)
        );

        if (book == null)
            return NotFound();

        return Ok(book);
    }

    /// <summary>
    /// Gets a paginated list of book, wheter or not using a author/title filter.
    /// </summary>
    /// 
    /// <param name="pageNumber">The number of the current page.
    /// <para>
    /// Defaults 1.
    /// </para>
    /// </param>
    /// 
    /// <param name="pageSize">
    /// The number of the item per page.
    /// <para>
    /// Defaults 10.
    /// </para>
    /// </param>
    /// 
    /// <param name="author">The name of the author to filter. Nullable</param>
    /// 
    /// <param name="title">The name of the book title to filter. Nullable</param>
    /// 
    /// <returns>
    /// A task representing asynchronous operation that the <see cref="OkResult"/> with the object found.
    /// </returns>
    /// 
    /// <response code="200">If the list of book was successfully found.</response>
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync([FromQuery] int pageNumber = 1,
     [FromQuery] int pageSize = 10,
     [FromQuery(Name = "author")] string? author = null,
     [FromQuery(Name = "title")] string? title = null)
    {
        var collection = await GetDataAsync(q => q.OrderBy(b => b.Title), b =>
            (string.IsNullOrEmpty(title) || b.Title.Contains(title)) &&
            (string.IsNullOrEmpty(author) || (b.Author != null && b.Author.LastName.Contains(author))), pageNumber, pageSize, author, title);

        return Ok(collection);
    }

    /// <summary>
    /// Updates an existing book in the current context.
    /// </summary>
    /// 
    /// <param name="id">The id of the entity.</param>
    /// 
    /// <param name="book">The DTO for updating book.</param>
    /// 
    /// <returns>
    /// A task representing asynchronous operation with the result of the updating.
    /// </returns>
    /// 
    /// <response code="204">If the book was successfully updated.</response>
    /// 
    /// <response code="404">If the book with the specified id was not found.</response>
    /// 
    /// <response code="400">If the data provided for updating book is invalid.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
}
