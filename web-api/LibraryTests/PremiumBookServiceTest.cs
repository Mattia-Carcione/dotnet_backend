using Context;
using Exceptions;
using Services;

namespace LibraryTests
{
    /// <summary>
    /// Tests for the PremiumBookService.
    /// </summary>
    public class PremiumBookServiceTest : IClassFixture<TestDatabaseFixture>
    {
        /// <summary>
        /// Gets the PremiumBookService instance used in tests.
        /// </summary>
        public PremiumBookService _service { get; private set; }

        /// <summary>
        /// Gets the LibraryContext instance used in tests.
        /// </summary>
        public LibraryContext _context { get; private set; }

        /// <summary>
        /// Gets the TestDatabaseFixture instance used in tests.
        /// </summary>
        public TestDatabaseFixture Fixture { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PremiumBookServiceTest"/> class.
        /// </summary>
        /// <param name="fixture">The test database fixture.</param>
        public PremiumBookServiceTest(TestDatabaseFixture fixture)
        {
            Fixture = fixture;

            _context = Fixture.CreateContext();

            _service = new(_context);
        }

        /// <summary>
        /// Tests if an order is successfully created for a premium user.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Fact]
        public async Task CreateOrder_Order_OrderIsCreated()
        {
            //Arrange
            var user = EntityFactoryHelper.CreateUser("jhon", "jhon@mail", isPremium: true);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            var book = _service.GetAsync(5);

            var initialOrderCount = _context.Orders.Count();

            //Act
            await _service.CreateOrderAsync("jhon@mail", book.Id);
            var finailOrderCount = _context.Orders.Count();

            //Assert
            Assert.Equal(initialOrderCount + 1, finailOrderCount);

        }

        /// <summary>
        /// Tests if creating an order with an invalid email throws a BookingException. 
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Fact]
        public async Task CreateOrder_Order_InvalidEmail()
        {
            //Act
            var task = async () => await _service.CreateOrderAsync("alice@jsj@", 1);

            //Assert
            await Assert.ThrowsAsync<BookingException>(async () => await task());
        }

        /// <summary>
        /// Tests if creating an order for a non-premium user throws a BookingException.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Fact]
        public async Task CreateOrder_Order_UserIsNotPremium()
        {
            //Arrange
            var user = EntityFactoryHelper.CreateUser("joe", "joe@mail", isPremium: false);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            //Act
            var task = async () => await _service.CreateOrderAsync("joe@mail", 1);

            //Assert
            await Assert.ThrowsAsync<BookingException>(async () => await task());
        }

        /// <summary>
        /// Tests if creating an order for a non-existent user throws a BookingException.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Fact]
        public async Task CreateOrder_Order_UserNotFound()
        {
            //Act
            var task = async () => await _service.CreateOrderAsync("fakeUser@mail", 1);

            //Assert
            await Assert.ThrowsAsync<BookingException>(async () => await task());
        }

        /// <summary>
        /// Tests if creating an order for a book that is not available throws a BookingException.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Fact]
        public async Task CreateOrder_Order_BookNotAvaiable()
        {
            //Arrange
            var user = EntityFactoryHelper.CreateUser("bart", "bart@mail", isPremium: true);
            await _context.AddAsync(user);
            await _context.AddAsync(EntityFactoryHelper.CreateBook("title", 1, 1, totalCopies: 0));
            await _context.SaveChangesAsync();
            var book = await _service.SearchEntityByCriteriaAsync(b => b.Where(b => b.TotalCopies == 0));

            //Act
            var task = async () => await _service.CreateOrderAsync("bart@mail", bookId: book.Id);

            //Assert
            await Assert.ThrowsAsync<BookingException>(async () => await task());
        }

        /// <summary>
        /// Tests if creating an order for a non-existent book throws a BookingException.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Fact]
        public async Task CreateOrder_Order_BookNotFound()
        {
            //Arrange
            var user = EntityFactoryHelper.CreateUser("boe", "boe@mail", isPremium: true);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            //Act
            var task = async () => await _service.CreateOrderAsync("boe@mail", bookId: 50);

            //Assert
            await Assert.ThrowsAsync<BookingException>(async () => await task());
        }

        /// <summary>
        /// Tests if the total copies of a book are updated correctly after an order is created.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Fact]
        public async Task CreateOrder_Order_BookCopiesIsUpdated()
        {
            //Arrange
            var user = EntityFactoryHelper.CreateUser("tracy", "tracy@mail", isPremium: true);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            var book = await _service.GetAsync(6);
            var initialBookCopies = book.TotalCopies;

            //Act
            await _service.CreateOrderAsync("tracy@mail", book.Id);
            var finalBookCopies = book.TotalCopies;

            //Assert
            Assert.Equal(initialBookCopies - 1, finalBookCopies);
        }
    }
}
