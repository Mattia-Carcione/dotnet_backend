/**
*TODO:
*c. Testare il servizio di prenotazione e consegna in modo da verificare tutti i
*casi d’uso:
*i. Un libro non può essere prenotato perchè non esiste. ++
*ii. Un libro non può essere prenotato perchè non più disponibile. ++
*iii. Un libro non può essere prenotato perchè l’utente lo ha già prenotato. ++
*iv. Un libro non può essere prenotato perchè l’utente ha già 3
*prenotazioni attive. ++
*v. Prenotazione avvenuta. Verifica che venga creato l’oggetto
*Prenotazione e che il numero di copie disponibili diminuisca di uno. ++
*vi. Un libro non può essere consegnato perchè:
*1. Non esiste una prenotazione sul libro ++
*2. Esiste la prenotazione sul libro ma non più attiva ++
*3. Esiste la prenotazione sul libro ancora attiva ma relativa ad un
*altro utente --
*vii. Un libro può essere consegnato. Verificare che la prenotazione non
*sia più attiva e che il numero di copie disponibili sia aumentato di uno. ++
*/

using Context;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using Services;

namespace LibraryTests;

/// <summary>
/// Tests methods of <see cref="BookService"/>.
/// </summary>
public class BookServiceTest : IClassFixture<TestDatabaseFixture>
{
    /// <summary>
    /// Gets the BookService instance used in tests.
    /// </summary>
    public BookService _service { get; private set; }

    /// <summary>
    /// Gets the TestDatabaseFixture instance used in tests.
    /// </summary>
    public TestDatabaseFixture Fixture { get; }

    /// <summary>
    /// Gets the LibraryContext instance used in tests.
    /// </summary>
    public LibraryContext _context { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BookServiceTest"/> class.
    /// </summary>
    /// <param name="fixture">The test database fixture.</param>
    public BookServiceTest(TestDatabaseFixture fixture)
    {
        Fixture = fixture;

        _context = Fixture.CreateContext();

        _service = new(_context);
    }

    /// <summary>
    /// Tests if a booking is successfully added.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task AddBooking_Booking_BookingIsAdded()
    {
        // Arrange
        var initialCount = await _context.Bookings.CountAsync();

        // Act
        await _service.BookingAsync("utenteTest@mail", 4);
        var finalCount = await _context.Bookings.CountAsync();

        // Assert
        Assert.Equal(initialCount + 1, finalCount);
    }

    /// <summary>
    /// Tests if the number of copies is updated correctly after a booking is added.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task AddBooking_Booking_BookCopiesIsUpdated()
    {
        // Arrange
        var book = await _service.GetAsync(1, query => query.Include(b => b.Bookings)) ?? throw new ArgumentNullException();
        var initialCopies = book.Copies;

        // Act
        await _service.BookingAsync("utenteTest17@mail", book.Id);
        var finalCopies = book.Copies;

        // Assert
        Assert.Equal(initialCopies - 1, finalCopies);
    }


    /// <summary>
    /// Tests if a booking is successfully updated.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task UpdateBookingTest_Booking_BookingIsUpdated()
    {
        // Arrange
        var book = await _service.GetAsync(1) ?? throw new ArgumentNullException();
        await _service.BookingAsync("utenteTest@mail", book.Id);
        var booking = book.Bookings.First(b => b.User != null && b.User.Email == "utenteTest@mail");
        var initialReturnDate = booking.ReturnDate;

        // Act
        await _service.UpdateBookingAsync("utenteTest@mail", booking.Id, book.Id);

        // Assert
        Assert.NotEqual(booking.ReturnDate, default);
        Assert.Equal(initialReturnDate, default);
    }

    /// <summary>
    /// Tests if the number of copies is updated correctly after a booking is updated.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task UpdateBookingTest_Booking_BookCopiesIsUpdated()
    {
        // Arrange
        var book = await _service.GetAsync(1) ?? throw new ArgumentNullException();
        var booking = await _service.BookingAsync("utenteTest@mail", book.Id);
        var initialCopies = book.Copies;

        // Act
        await _service.UpdateBookingAsync("utenteTest@mail", booking.Id, book.Id);
        var finalCopies = book.Copies;

        // Assert
        Assert.Equal(initialCopies + 1, finalCopies);
    }

    /// <summary>
    /// Tests if booking a non-existent book throws a BookingException.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task AddBooking_Booking_BookNotFound()
    {
        // Arrange
        var fakeBook = EntityFactoryHelper.CreateBook("test", 1, 1);

        // Act
        var booking = async () => await _service.BookingAsync("utenteTest@mail", fakeBook.Id);

        // Assert
        await Assert.ThrowsAsync<BookingException>(async () => await booking());
    }

    /// <summary>
    /// Tests if booking a book with zero copies throws a BookingException.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task AddBooking_Booking_BookNotAvailable()
    {
        // Arrange
        var bookNotAvailable = EntityFactoryHelper.CreateBook("test", 1, 1, copies: 0);
        await _service.AddAsync(bookNotAvailable);
        await _service.SaveChangesAsync();

        // Act
        var booking = async () => await _service.BookingAsync("utenteTest@mail", bookNotAvailable.Id);

        // Assert
        await Assert.ThrowsAsync<BookingException>(async () => await booking());
    }

    /// <summary>
    /// Tests if booking a book that is already booked by the user throws a BookingException.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task AddBooking_Booking_ExistingBooking()
    {
        // Arrange
        var book = EntityFactoryHelper.CreateBook("test", 1, 1, copies: 10);
        await _service.AddAsync(book);
        await _service.SaveChangesAsync();
        await _service.BookingAsync("utenteTest@mail", book.Id);

        // Act
        var booking = async () => await _service.BookingAsync("utenteTest@mail", book.Id);

        // Assert
        await Assert.ThrowsAsync<BookingException>(async () => await booking());
    }

    /// <summary>
    /// Tests if booking a fourth book when the user already has three active bookings throws a BookingException.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task AddBooking_Booking_TooManyBookings()
    {
        // Arrange
        var book1 = EntityFactoryHelper.CreateBook("test", 1, 1, copies: 10);
        var book2 = EntityFactoryHelper.CreateBook("test", 1, 1, copies: 10);
        var book3 = EntityFactoryHelper.CreateBook("test", 1, 1, copies: 10);
        var book4 = EntityFactoryHelper.CreateBook("test", 1, 1, copies: 10);
        await _service.AddAsync(book1);
        await _service.AddAsync(book2);
        await _service.AddAsync(book3);
        await _service.AddAsync(book4);
        await _service.SaveChangesAsync();
        await _service.BookingAsync("utenteTest9@mail", book1.Id);
        await _service.BookingAsync("utenteTest9@mail", book2.Id);
        await _service.BookingAsync("utenteTest9@mail", book3.Id);

        // Act
        var booking = async () => await _service.BookingAsync("utenteTest9@mail", book4.Id);

        // Assert
        await Assert.ThrowsAsync<BookingException>(async () => await booking());
    }

    /// <summary>
    /// Tests if updating a booking for a book that hasn't been booked by the user throws a BookingException.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task UpdateBookingTest_Booking_BookHasntBooked()
    {
        // Arrange
        var booking = await _service.BookingAsync("utenteTest5@mail", 1);

        // Act
        var updateBooking = async () => await _service.UpdateBookingAsync("utenteTest5@mail", booking.Id, 5);

        // Assert
        await Assert.ThrowsAsync<BookingException>(async () => await updateBooking());
    }

    /// <summary>
    /// Tests if updating a booking for a book that has already been returned throws a BookingException.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task UpdateBookingTest_Booking_BookHasReturned()
    {
        // Arrange
        var booking = await _service.BookingAsync("utenteTest20@mail", 1);
        await _service.UpdateBookingAsync("utenteTest20@mail", booking.Id, 1);

        // Act
        var updateBooking = async () => await _service.UpdateBookingAsync("utenteTest20@mail", booking.Id, 1);

        // Assert
        await Assert.ThrowsAsync<BookingException>(async () => await updateBooking());
    }

    /// <summary>
    /// Tests if updating a booking with a mismatched booking ID and user throws a BookingException.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task UpdateBookingTest_Booking_BookingAndUserMismatch()
    {
        // Arrange
        var booking = await _service.BookingAsync("testUser3@mail", 3);
        var booking1 = await _service.BookingAsync("test_user@mail", 1);

        // Act
        var updateBooking = async () => await _service.UpdateBookingAsync("test_user@mail", booking.Id, booking.Book.Id);

        // Assert
        await Assert.ThrowsAsync<BookingException>(async () => await updateBooking());
    }
}
