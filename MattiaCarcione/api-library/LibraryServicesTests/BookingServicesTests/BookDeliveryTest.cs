using LibraryModel.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServicesTests.BookingServicesTests
{
    public class BookDeliveryTest : IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDatabaseFixture _fixture;

        public BookDeliveryTest(TestDatabaseFixture testDatabaseFixture)
        {
            _fixture = testDatabaseFixture;
        }

        [Fact]
        public async Task BookDelivery_AddBookToDelivery_ReturnUpdateBooking()
        {
            //Arrange
            var author = new Author() { FirstName = "Test", LastName = "test" };
            var publisher = new Publisher() { Name = "Test" };
            var book = new Book() { Author = author, Publisher = publisher, Title = "test", TotalCopies = 20, TotalCopiesLeft = 19 };
            var booking = new Booking() { Book = book, User = "test" };

            //Act
            using (var context = _fixture.CreateContext())
            {
                await context.Bookings.AddAsync(booking);
                await context.SaveChangesAsync();

                var userBookings = await context.Bookings.Where(booking => booking.User == booking.User).Include(b => b.Book).ToListAsync();

                var userBooking = userBookings.Where(booking => booking.BookingID == booking.BookingID && booking.Book.BookID == book.BookID).First();

                if (userBooking.Book.TotalCopiesLeft + 1 > userBooking.Book.TotalCopies)
                {
                    userBooking.Book.TotalCopiesLeft = userBooking.Book.TotalCopies;
                }
                else
                {
                    userBooking.Book.TotalCopiesLeft += 1;
                }

                userBooking.DeliveryDate = DateTime.Now;
                context.Bookings.Update(userBooking);
                await context.SaveChangesAsync();

                var delivery = await context.Bookings.Where(b => b.DeliveryDate == userBooking.DeliveryDate).FirstAsync();
                var deliveryBook = await context.Books.FirstAsync();

                //Assert
                Assert.Equal(userBooking.DeliveryDate, delivery.DeliveryDate);
                Assert.False(delivery.DeliveryDate == default);
                Assert.Equal(20, deliveryBook.TotalCopiesLeft);
                await context.DisposeAsync();
            }
        }

        [Fact]
        public async Task DeliveryTest_AddNonExisistingBooking_ReturnFalse()
        {
            //Arrange
            var author = new Author() { FirstName = "Test", LastName = "test" };
            var publisher = new Publisher() { Name = "Test" };
            var book = new Book() { Author = author, Publisher = publisher, Title = "testbook", TotalCopies = 20, TotalCopiesLeft = 19 };
            var notBookingBook = new Book() { Author = author, Publisher = publisher, Title = "test1", TotalCopies = 20, TotalCopiesLeft = 19 };
            var booking = new Booking() { User = "test", Book = notBookingBook };

            //Act
            using (var context = _fixture.CreateContext())
            {
                await context.Books.AddAsync(book);
                await context.Bookings.AddAsync(booking);
                await context.SaveChangesAsync();
                var bookQuery = await context.Books.Where(b => b.Title == book.Title).FirstAsync();
                var bookingQuery = await context.Bookings.FirstAsync();

                var userBookings = await context.Bookings.Where(booking => booking.User == bookingQuery.User).Include(b => b.Book).ToListAsync();

                var userBooking = userBookings.Where(booking => booking.BookingID == bookingQuery.BookingID && booking.Book.BookID == bookQuery.BookID).Any();

                //Assert
                Assert.False(userBooking);
                await context.DisposeAsync();
            }
        }

        [Fact]
        public async Task DeliveryTest_AddExistingDelivery_ReturnFalse()
        {
            //Arrange
            var author = new Author() { FirstName = "Test", LastName = "test" };
            var publisher = new Publisher() { Name = "Test" };
            var book = new Book() { Author = author, Publisher = publisher, Title = "test", TotalCopies = 20, TotalCopiesLeft = 19 };
            var booking = new Booking() { User = "test", Book = book, DeliveryDate = DateTime.Now };

            //Act
            using (var context = _fixture.CreateContext())
            {
                await context.Books.AddAsync(book);
                await context.Bookings.AddAsync(booking);
                await context.SaveChangesAsync();
                var bookQuery = await context.Books.FirstAsync();
                var bookingQuery = await context.Bookings.FirstAsync();

                var userBookings = await context.Bookings.Where(booking => booking.User == booking.User).Include(b => b.Book).ToListAsync();

                bool isUserBookings = userBookings.Any(booking => booking.BookingID == bookingQuery.BookingID && booking.DeliveryDate == default);

                //Assert
                Assert.False(isUserBookings);
                await context.DisposeAsync();
            }
        }

        [Fact]
        public async Task DeliveryTest_AddOtherUserBooking_ReturnFalse()
        {
            //Arrange
            var author = new Author() { FirstName = "Test", LastName = "test" };
            var publisher = new Publisher() { Name = "Test" };
            var book = new Book() { Author = author, Publisher = publisher, Title = "test", TotalCopies = 20, TotalCopiesLeft = 19 };
            var booking = new Booking() { User = "test", Book = book, DeliveryDate = DateTime.Now };
            var user = "user";

            //Act
            using (var context = _fixture.CreateContext())
            {
                await context.Books.AddAsync(book);
                await context.Bookings.AddAsync(booking);
                await context.SaveChangesAsync();
                var bookQuery = await context.Books.FirstAsync();
                var bookingQuery = await context.Bookings.FirstAsync();

                var userBookings = await context.Bookings.Where(booking => booking.User == user).Include(b => b.Book).ToListAsync();

                bool isUserBookings = userBookings.Any(booking => booking.BookingID == bookingQuery.BookingID && booking.DeliveryDate == default);

                //Assert
                Assert.False(isUserBookings);
                await context.DisposeAsync();
            }
        }
    }
}
