using Context;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace LibraryTests;

public class TestDatabaseFixture
{
    private const string ConnectionString =
        @"Server=(localdb)\mssqllocaldb;Database=EFTestSample;Trusted_Connection=True;ConnectRetryCount=0";

    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public TestDatabaseFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    var author = new Author
                    {
                        Name = "Test",
                        LastName = "Test",
                        BirthDate = new DateTime(1970, 1, 1),
                    };

                    var editor = new Editor { Name = "Test Editor" };

                    var category = new Category
                    {
                        Genre = "Fiction",
                        Description = "Fictional Books"
                    };

                    var book1 = new Book
                    {
                        Title = "Test Book 1",
                        Pages = 365,
                        TotalCopies = 10,
                        Copies = 10,
                        PublicationDate = DateTime.Today,
                        Author = author,
                        Editor = editor
                    };
                    book1.AddCategory(category);

                    var book2 = new Book
                    {
                        Title = "Test Book 2",
                        Pages = 250,
                        TotalCopies = 5,
                        Copies = 5,
                        PublicationDate = DateTime.Today.AddYears(-1),
                        Author = author,
                        Editor = editor
                    };
                    book2.AddCategory(category);

                    var book3 = new Book
                    {
                        Title = "Test Book 3",
                        Pages = 255,
                        TotalCopies = 5,
                        Copies = 5,
                        PublicationDate = DateTime.Today.AddYears(-1),
                        Author = author,
                        Editor = editor
                    };
                    book3.AddCategory(category);

                    var booking1 = new Booking
                    {
                        User = "test_user",
                        BookingDate = DateTime.Today.AddDays(-10),
                        Book = book1
                    };

                    var booking2 = new Booking
                    {
                        User = "test_user",
                        BookingDate = DateTime.Today.AddDays(-5),
                        Book = book2
                    };

                    var booking3 = new Booking
                    {
                        User = "test_user",
                        BookingDate = DateTime.Today,
                        Book = book3
                    };

                    context.AddRange(author, editor, category, book1, book2, book3, booking1, booking2, booking3);
                    context.SaveChanges();
                }

                _databaseInitialized = true;
            }
        }
    }

    public LibraryContext CreateContext() =>
        new LibraryContext(
            new DbContextOptionsBuilder<LibraryContext>().UseSqlServer(ConnectionString).Options
        );
}
