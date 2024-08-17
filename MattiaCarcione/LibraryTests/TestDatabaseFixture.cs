using Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryTests;

public class TestDatabaseFixture : EntityFactoryHelper
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

                    var author = CreateAuthor("Test", "Test");
                    var editor = CreateEditor("Test Editor");
                    var category = CreateCategory("Fiction", "Fictional Books");

                    var book1 = CreateBook(
                        "Test Book 1",
                        author.Id,
                        editor.Id,
                        365,
                        10,
                        10,
                        DateTime.Today
                    );
                    book1.AddCategory(category);
                    book1.Author = author;
                    book1.Editor = editor;

                    var book2 = CreateBook(
                        "Test Book 2",
                        author.Id,
                        editor.Id,
                        250,
                        5,
                        5,
                        DateTime.Today.AddYears(-1)
                    );
                    book2.AddCategory(category);
                    book2.Author = author;
                    book2.Editor = editor;

                    var book3 = CreateBook(
                        "Test Book 3",
                        author.Id,
                        editor.Id,
                        255,
                        5,
                        5,
                        DateTime.Today.AddYears(-1)
                    );
                    book3.AddCategory(category);
                    book3.Author = author;
                    book3.Editor = editor;

                    var booking1 = CreateBooking("test_user", book1.Id);
                    booking1.Book = book1;
                    var booking2 = CreateBooking("test_user", book2.Id);
                    booking2.Book = book2;
                    var booking3 = CreateBooking("test_user", book3.Id);
                    booking3.Book = book3;

                    context.AddRange(
                        author,
                        editor,
                        category,
                        book1,
                        book2,
                        book3,
                        booking1,
                        booking2,
                        booking3
                    );
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
