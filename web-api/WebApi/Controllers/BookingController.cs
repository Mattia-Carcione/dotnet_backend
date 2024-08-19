/*
*TODO:
*Aggiungere alla solution una web API che espone 
- Crea prenotazione
- Consegna
*/

using DTOs.BookingDTOs;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/books")]
public class BookingController : ControllerBase
{
    private readonly IBookService _bookService;
    public BookingController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost("booking")]
    public async Task<IActionResult> BookingAsync([FromBody] CreateBookingDTO booking)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _bookService.BookingAsync(booking.User, booking.BookId);

        return NoContent();
    }

    [HttpPost("delivering")]
    public async Task<IActionResult> DeliveryAsync([FromBody] CreateDeliveryDTO delivery)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _bookService.DeliveryAsync(delivery.User, delivery.BookingId, delivery.BookId);

        return NoContent();
    }
}
