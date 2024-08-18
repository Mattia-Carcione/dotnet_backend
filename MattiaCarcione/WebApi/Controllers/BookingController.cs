using DTOs.BookingDTOs;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookService _bookService;
    public BookingController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost("/booking")]
    public async Task<IActionResult> Booking([FromBody] CreateBookingDTO booking)
    {
        if (!ModelState.IsValid || booking.User == null)
            return BadRequest(ModelState);

        await _bookService.BookingAsync(booking.User, booking.BookId);

        return Ok();
    }

    [HttpPost("/delivery")]
    public async Task<IActionResult> Delivery([FromBody] CreateDeliveryDTO delivery)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _bookService.DeliveryAsync(delivery.User, delivery.BookingId, delivery.BookId);

        return Ok();
    }
}
