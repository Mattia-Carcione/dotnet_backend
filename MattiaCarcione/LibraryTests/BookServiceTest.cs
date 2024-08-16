/**
*TODO:
*c. Testare il servizio di prenotazione e consegna in modo da verificare tutti i
*casi d’uso:
*i. Un libro non può essere prenotato perchè non esiste.
*ii. Un libro non può essere prenotato perchè non più disponibile.
*iii. Un libro non può essere prenotato perchè l’utente lo ha già prenotato.
*iv. Un libro non può essere prenotato perchè l’utente ha già 3
*prenotazioni attive.
*v. Prenotazione avvenuta. Verifica che venga creato l’oggetto
*Prenotazione e che il numero di copie disponibili diminuisca di uno.
*vi. Un libro non può essere consegnato perchè:
*1. Non esiste una prenotazione sul libro
*2. Esiste la prenotazione sul libro ma non più attiva
*3. Esiste la prenotazione sul libro ancora attiva ma relativa ad un
*altro utente
*vii. Un libro può essere consegnato. Verificare che la prenotazione non
*sia più attiva e che il numero di copie disponibili sia aumentato di uno.
*/

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
        var book = await _service.GetAsync(1);
        var bookingsCount = book.Bookings.Count;

        // //Act
        await _service.BookingAsync("utenteTest", book.ID);
        var updatedBook = await _service.GetAsync(1);
        var updatedBookingsCount = updatedBook.Bookings.Count;

        //Assert
        Assert.Equal(bookingsCount + 1, updatedBookingsCount);
    }

//TODO: 
//Da finire questo metodo e implementare gli ultimi del test
//Aggiungere a questo metodo l'id del libro
    [Fact]
    public async Task DeliveryTest_Booking_BookingIsUpdated()
    {
        //Assign
        var book = await _service.GetAsync(1);

        // //Act
        await _service.DeliveryAsync("test_user", book.ID);
        var updatedBook = await _service.GetAsync(1);
        // var updatedBookingsCount = updatedBook.Bookings.Count;

        //Assert
        // Assert.Equal(bookingsCount + 1, updatedBookingsCount);
    }
}
