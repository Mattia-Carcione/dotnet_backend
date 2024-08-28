/*
*TODO:
*Aggiungere alla solution una web API che espone
- Crea prenotazione
- Consegna
- cerca prenotazioni
- cerca singola prenotazione per utente
- cerca prenotazioni per libro
*/

using System.Text.Json;
using AutoMapper;
using DTOs.BookingDTOs;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Metadatas;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/bookings")]
public class BookingController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IExtendedRepository<Booking> _repository;
    private readonly IMapper _mapper;
    private const int MaxPageSize = 25;

    public BookingController(IBookService bookService,
        IExtendedRepository<Booking> repository,
        IMapper mapper)
    {
        _bookService = bookService;
        _repository = repository;
        _mapper = mapper;
    }

    private async Task<(IEnumerable<BookingDetailDTO>, PaginationMetadata)> GetAllBookingsAsync(int pageNumber, int pageSize)
    {
        var (bookings, paginationMetadata) = await _repository.GetAllAsync(pageNumber, pageSize, q => 
            q.Include(b => b.Book).OrderByDescending(b => b.BookingDate));

        var mappedBookings = _mapper.Map<IEnumerable<BookingDetailDTO>>(bookings);

        return (mappedBookings, paginationMetadata);
    }

    [HttpPost("create")]
    public async Task<IActionResult> BookingAsync([FromBody] CreateBookingDTO booking)
    {
        var newBooking = await _bookService.BookingAsync(booking.User, booking.BookId);
        var mappedBooking = _mapper.Map<BookingDetailDTO>(newBooking);

        return CreatedAtRoute("GetBookingAsync", new { id = mappedBooking.Id }, mappedBooking);
    }

    [HttpGet("{id}", Name = "GetBookingAsync")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        var booking = await _repository.GetAsync(id, include: q => 
            q.Include(b => b.Book));

        var mappedBooking = _mapper.Map<BookingDetailDTO>(booking);

        return Ok(mappedBooking);
    }

    [HttpGet()]
    public async Task<IActionResult> SearchByCriteriaAsync([FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery(Name = "user")] string? user = null,
        [FromQuery(Name = "title")] string? title = null)
    {
        pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            
        if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(user))
        {
            var (collection, paginationMetadata) = await GetAllBookingsAsync(pageNumber, pageSize);
            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            return Ok(collection);
        }

        title = title?.Trim();
        user = user?.Trim();

        var (bookings, pagination_metadata) = await _repository.SearchByCriteriaAsync(pageNumber, pageSize, b =>
                (string.IsNullOrEmpty(title) || b.Book.Title.Contains(title)) &&
                (string.IsNullOrEmpty(user) || (b.User != null && b.User.Contains(user))),
            q => q.Include(b => b.Book)
                .OrderByDescending(b => b.BookingDate)
        );

        var mappedBookings = _mapper.Map<IEnumerable<BookingDetailDTO>>(bookings);
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagination_metadata));
        
        return Ok(mappedBookings);
    }

    [HttpPost("delivering")]
    public async Task<IActionResult> DeliveryAsync([FromBody] CreateDeliveryDTO delivery)
    {
        await _bookService.DeliveryAsync(delivery.User, delivery.BookingId, delivery.BookId);

        return NoContent();
    }
}
