using Entities.Data;
using Mapper.Reservations;
using Mapper.Books;
using Interfaces.crud_entities;
using Dtos.Reservations;
using Microsoft.AspNetCore.Mvc;
using Dtos.BookDtos;
using Intefaces.crud.entities;

namespace WebAPI.Controller
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {

        private readonly Library_DbContext _context;
        private readonly IReservationRepository _reserveRepo;
        private readonly IBookRepository _bookRepo;
        public ReservationController(Library_DbContext context, IReservationRepository reserveRepo, IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
            _reserveRepo = reserveRepo;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var revervations =  await _reserveRepo.GetAllAsync();
            var reservationDto = revervations.Select(s => s.ToReservationDto());
            return Ok(reservationDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var reservation = await _reserveRepo.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation.ToReservationDto());
        }
        [HttpPost]
        public async Task<IActionResult> ReturnBook()
        {

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var book = await _bookRepo.GetByTitleAsync(request.Title);
            if (book == null)
            {
                return NotFound("Book not found.");
            }
            var reservationModel = await _reserveRepo.CreateTestAsync(request.User, book);
            await _reserveRepo.CreateAsync(reservationModel);
            return CreatedAtAction(nameof(GetById), new { id = reservationModel.ReservationId }, reservationModel.ToReservationDto());
        }




    }
}
