using Microsoft.AspNetCore.Mvc;
using LibraryServices.Services.Read.ReadBooking;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await ReadBookings.GetAllBookings();

            return Ok(bookings);
        }

        [HttpGet("user={user}")]
        public async Task<IActionResult> GetSearchBookingsByUser([FromRoute] string user)
        {
            var bookings = await ReadBookingsByUser.SearchBookingsByUser(user);

            return Ok(bookings);
        }

        [HttpGet("booking={id}")]
        public async Task<IActionResult> GetSearchUserByBooking([FromRoute] int id)
        {
            var bookings = await ReadUserByBooking.SearchUserByBooking(id);

            return Ok(bookings);
        }
    }
}
