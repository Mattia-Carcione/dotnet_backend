/*
*TODO:
*Aggiungere alla solution una web API che espone
- Crea prenotazione
- Consegna
- cerca prenotazioni
- cerca singola prenotazione per utente
- cerca prenotazioni per libro
*/

using Asp.Versioning;
using AutoMapper;
using DTOs.BookingDTOs;
using DTOs.OrderDTOs;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace WebApi.Controllers;

/// <summary>
/// Controller provides booking-related operation.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/bookings")]
[ApiVersion(1)]
public class BookingController : ControllerHelper<Booking, BookingDetailDTO, BookingDetailDTO>
{
    /// <summary>
    /// A interface of booking-related services.
    /// </summary>
    private readonly IBookService _bookService;

    /// <summary>
    /// Initializes a new instance of <see cref="BookingController"/>.
    /// </summary>
    /// 
    /// <param name="bookService">An object of <see cref="IBookService"/>.</param>
    /// 
    /// <param name="repository">The repository interface that provides the CRUD operation methods.</param>
    /// 
    /// <param name="mapper">A mapper object that maps entities to each other.</param>
    public BookingController(IFactoryService<IBookService> bookService,
        IExtendedRepository<Booking> repository,
        IMapper mapper) : base(mapper, repository)
    {
        _bookService = bookService.CreateService().Result;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Booking"/>.
    /// </summary>
    /// 
    /// <param name="booking">The object DTO for creating booking.</param>
    /// 
    /// <returns>A task representing the asynchronous operation for creating a new booking.</returns>
    /// 
    /// <response code="201">If the booking was created correctly.</response>
    /// 
    /// <response code="400">If the data provided for the creation of the booking is invalid.</response>
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BookingAsync([FromBody] CreateBookingDTO booking)
    {
        var newBooking = await _bookService.BookingAsync(booking.Email, booking.BookId);
        var mappedBooking = _mapper.Map<BookingDetailDTO>(newBooking);

        return CreatedAtRoute("GetBookingAsync", new { id = mappedBooking.Id }, mappedBooking);
    }

    /// <summary>
    /// Gets the item with the specified id.
    /// </summary>
    /// 
    /// <param name="id">The id of the entity.</param>
    /// 
    /// <returns>A task representing asynchronous operation that returns the <see cref="OkResult"/> with the object created; else, <see cref="NotFoundResult"/>.</returns>
    /// 
    /// <response code="200">If the booking was successfully found.</response>
    /// 
    /// <response code="404">If the booking doesn't exist in the current context.</response>
    [HttpGet("{id}", Name = "GetBookingAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookingAsync([FromRoute] int id)
    {
        var booking = await GetAsync(id, q =>
            q.Include(b => b.Book));

        if (booking == null)
            return NotFound();

        return Ok(booking);
    }

    /// <summary>
    /// Gets a paginated list of booking, wheter or not using a user/title filter.
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
    /// <param name="user">The name of the user to filter. Nullable</param>
    /// 
    /// <param name="title">The name of the book title to filter. Nullable</param>
    /// 
    /// <returns>
    /// A task representing asynchronous operation that the <see cref="OkResult"/> with the object found.
    /// </returns>
    /// 
    /// <response code="200">If the list of booking was successfully found.</response>
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync([FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery(Name = "user")] string? user = null,
        [FromQuery(Name = "title")] string? title = null)
    {
        var collection = await GetDataAsync(q => q.Include(b => b.Book).OrderByDescending(b => b.BookingDate), b =>
                (string.IsNullOrEmpty(title) || (b.Book != null && b.Book.Title.Contains(title))) &&
                (string.IsNullOrEmpty(user) || (b.User != null && b.User.Email.Contains(user))), pageNumber, pageSize, user, title);

        return Ok(collection);
    }

    /// <summary>
    /// Updates an existing booking in the current context.
    /// </summary>
    /// 
    /// <param name="bookingId">The id of the entity.</param>
    /// 
    /// <param name="returnDTO">The DTO for updating booking.</param>
    /// 
    /// <returns>
    /// A task representing asynchronous operation with the result of the updating.
    /// </returns>
    /// 
    /// <response code="204">If the booking was successfully updated.</response>
    /// 
    /// <response code="404">If the booking with the specified id was not found.</response>
    /// 
    /// <response code="400">If the data provided for updating booking is invalid.</response>
    [HttpPut("{bookingId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBookingAsync([FromRoute] int bookingId, [FromBody] UpdateBookingDTO returnDTO)
    {
        if (await _repository.GetAsync(bookingId) == null)
            return NotFound("Booking not found");

        await _bookService.UpdateBookingAsync(returnDTO.Email, bookingId, returnDTO.BookId);

        return NoContent();
    }

    /// <summary>
    /// Creates a new instance of <see cref="Order"/> for a premium member.
    /// </summary>
    /// <param name="orderToCreate">The DTO for creating a new order.</param>
    /// <returns>
    /// A task representing asynchronous operation that returns a result of <see cref="IActionResult"/>.
    /// <list type="bullet">
    /// <item>
    /// <see cref="OkObjectResult"/> that produces an <see cref="StatusCodes.Status200OK"/> if the order was created,
    /// </item>
    /// <item>
    /// <see cref="UnauthorizedResult"/> that produces an <see cref="StatusCodes.Status401Unauthorized"/> if the user is a standard premium.
    /// </item>
    /// </list>
    /// </returns>
    [HttpPost("buy-book")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> BuyBookAsync([FromBody] CreateOrderDTO orderToCreate)
    {
        Order order;

        if (_bookService is IPremiumBookService _premiumService)
        {
            order = await _premiumService.CreateOrderAsync(orderToCreate.Email, orderToCreate.BookId);

            var mappedOrder = _mapper.Map<OrderDTO>(order);

            return Ok(mappedOrder);
        }

        return Unauthorized();
    }
}
