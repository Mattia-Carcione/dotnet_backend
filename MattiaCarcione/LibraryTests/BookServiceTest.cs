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

using Exceptions;
using Services;

namespace LibraryTests;

public class BookServiceTest : IClassFixture<TestDatabaseFixture>
{
    public BookService _service { get; private set; }
    public TestDatabaseFixture Fixture { get; }

    public BookServiceTest(TestDatabaseFixture fixture)
    {
        Fixture = fixture;

        var context = Fixture.CreateContext();

        _service = new(context);
    }

    [Fact]
    public async Task AddBooking_Booking_BookingIsAdded()
    {
        //Assign
        var initialBook = await _service.GetAsync(1);
        var initialBookingsCount = initialBook.Bookings.Count;
        var initialCopies = initialBook.Copies;

        // //Act
        await _service.BookingAsync("utenteTest", initialBook.Id);
        var finalBook = await _service.GetAsync(1);
        var finalBookingsCount = finalBook.Bookings.Count;
        var finalCopies = finalBook.Copies;

        //Assert
        Assert.Equal(initialBookingsCount + 1, finalBookingsCount);
        Assert.Equal(initialCopies - 1, finalCopies);
    }

    [Fact]
    public async Task DeliveryTest_Booking_BookingIsUpdated()
    {
        //Assign
        var book = await _service.GetAsync(1);
        await _service.BookingAsync("utenteTest", book.Id);
        var initialCopies = book.Copies;
        var booking = book.Bookings.First(b => b.User == "utenteTest");
        var initialDeliveryDate = booking.DeliveryDate;

        // //Act
        await _service.DeliveryAsync("utenteTest", booking.Id, book.Id);

        //Assert
        Assert.Equal(book.Copies, initialCopies + 1);
        Assert.NotEqual(booking.DeliveryDate, default);
        Assert.Equal(initialDeliveryDate, default);
    }

    [Fact]
    public async Task AddBooking_Booking_BookNotFound()
    {
        //Assign
        var fakeBook = Fixture.CreateBook("test", 1, 1);

        // //Act
        var booking = async () => await _service.BookingAsync("utenteTest", fakeBook.Id);

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await booking());
    }

    [Fact]
    public async Task AddBooking_Booking_BookNotAvailable()
    {
        //Assign
        var bookNotAvailable = Fixture.CreateBook("test", 1, 1, copies: 0);
        await _service.AddAsync(bookNotAvailable);
        await _service.SaveChangesAsync();

        // //Act
        var booking = async () => await _service.BookingAsync("utenteTest", bookNotAvailable.Id);

        //Assert
        await Assert.ThrowsAsync<BookingException>(async () => await booking());
    }

    [Fact]
    public async Task AddBooking_Booking_ExistingBooking()
    {
        //Assign
        var book = Fixture.CreateBook("test", 1, 1, copies: 10);
        await _service.AddAsync(book);
        await _service.SaveChangesAsync();
        await _service.BookingAsync("utenteTest", book.Id);

        // //Act
        var booking = async () => await _service.BookingAsync("utenteTest", book.Id);

        //Assert
        await Assert.ThrowsAsync<BookingException>(async () => await booking());
    }

    [Fact]
    public async Task AddBooking_Booking_ToManyBookings()
    {
        //Assign
        var book1 = Fixture.CreateBook("test", 1, 1, copies: 10);
        var book2 = Fixture.CreateBook("test", 1, 1, copies: 10);
        var book3 = Fixture.CreateBook("test", 1, 1, copies: 10);
        var book4 = Fixture.CreateBook("test", 1, 1, copies: 10);
        await _service.AddAsync(book1);
        await _service.AddAsync(book2);
        await _service.AddAsync(book3);
        await _service.AddAsync(book4);
        await _service.SaveChangesAsync();
        await _service.BookingAsync("utenteTest", book1.Id);
        await _service.BookingAsync("utenteTest", book2.Id);
        await _service.BookingAsync("utenteTest", book3.Id);

        // //Act
        var booking = async () => await _service.BookingAsync("utenteTest", book4.Id);

        //Assert
        await Assert.ThrowsAsync<BookingException>(async () => await booking());
    }

    [Fact]
    public async Task DeliveryTest_Booking_BookHasntBooked()
    {
        //Assign
        var book = Fixture.CreateBook("test", 1, 1, copies: 10);
        await _service.AddAsync(book);
        await _service.SaveChangesAsync();

        // //Act
        var delivery = async () => await _service.DeliveryAsync("User1", 1, book.Id);

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await delivery());
    }

    [Fact]
    public async Task DeliveryTest_Booking_BookHasReturned()
    {
        //Assign
        await _service.DeliveryAsync("User1", 1, 1);

        // //Act
        var delivery = async () => await _service.DeliveryAsync("User1", 1, 1);

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await delivery());
    }

    [Fact]
    public async Task DeliveryTest_Booking_BookingAndUserDoNotMatch()
    {
        //Assign
        var book = Fixture.CreateBook("test", 1, 1, copies: 10);
        await _service.AddAsync(book);
        await _service.SaveChangesAsync();
        await _service.BookingAsync("testUser", book.Id);
        var booking = book.Bookings.First(b => b.User == "testUser");

        // //Act
        var delivery = async () => await _service.DeliveryAsync("test_user", booking.Id, book.Id);

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await delivery());
    }
}
