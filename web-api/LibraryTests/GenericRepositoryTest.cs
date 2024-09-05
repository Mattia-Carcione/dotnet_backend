/**
*TODO:
*a. Test sui metodi CRUD degli elementi, in particolare del libro.
*
*b. Test di salvataggio di un libro contenente diverse categorie. Aggiungere e
*togliere categorie al libro e verificare che il salvataggio funzioni
*correttamente.
**/

using Context;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repository;

namespace LibraryTests;

/// <summary>
/// Tests methods of <see cref="GenericRepository{T, TContext}"/>.
/// </summary>
public class GenericRepositoryTest : IClassFixture<TestDatabaseFixture>
{
    /// <summary>
    /// Gets the GenericRepository instance used in tests.
    /// </summary>
    public GenericRepository<Book, LibraryContext> _repository { get; private set; }

    /// <summary>
    /// Gets the TestDatabaseFixture instance used in tests.
    /// </summary>
    public TestDatabaseFixture Fixture { get; }

    /// <summary>
    /// Gets the LibraryContext instance used in tests.
    /// </summary>
    public LibraryContext _context { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepositoryTest"/> class.
    /// </summary>
    /// <param name="fixture">The test database fixture.</param>
    public GenericRepositoryTest(TestDatabaseFixture fixture)
    {
        Fixture = fixture;

        _context = Fixture.CreateContext();

        _repository = new(_context);
    }

    /// <summary>
    /// Tests if a book is successfully added to the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task AddBook_Book_BookIsAdded()
    {
        // Arrange
        var initialCount = await _context.Books.CountAsync();
        Book book = EntityFactoryHelper.CreateBook("test", 1, 1);

        // Act
        await _repository.AddAsync(book);
        await _repository.SaveChangesAsync();
        var finalCount = await _context.Books.CountAsync();

        // Assert
        Assert.Equal(initialCount + 1, finalCount);
    }

    /// <summary>
    /// Tests if a book's information is successfully updated in the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task UpdateBook_Book_BookIsUpdated()
    {
        // Arrange
        Book book = await _repository.GetAsync(1) ?? throw new ArgumentNullException();
        var initialCopies = book.TotalCopies;
        book.TotalCopies++;

        // Act
        _repository.Update(book);
        await _repository.SaveChangesAsync();
        var updatedBook = await _repository.GetAsync(book.Id) ?? throw new ArgumentNullException();

        // Assert
        Assert.Equal(initialCopies + 1, updatedBook.TotalCopies);
    }

    /// <summary>
    /// Tests if a book is successfully removed from the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task DeleteBook_Book_BookIsRemoved()
    {
        // Arrange
        var (initialList, metadata) = await _repository.GetAllAsync(1, 10, c => c.OrderBy(b => b.Id));
        var count = initialList.Count();
        Book book = await _repository.GetAsync(2) ?? throw new ArgumentNullException();

        // Act
        _repository.Delete(book);
        await _repository.SaveChangesAsync();
        var (finalList, metadata2) = await _repository.GetAllAsync(1, 10, c => c.OrderBy(b => b.Id));

        // Assert
        Assert.Equal(count - 1, finalList.Count());
    }

    /// <summary>
    /// Tests if the repository returns a list of books.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task GetAllBook_Book_GetListOFBook()
    {
        // Arrange
        var (initialList, metadata) = await _repository.GetAllAsync(1, 10, c => c.OrderBy(b => b.Id));

        // Act
        var count = initialList.Count();

        // Assert
        Assert.True(count > 0);
    }

    /// <summary>
    /// Tests if the repository returns a specific book by ID.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Fact]
    public async Task GetBook_Book_GetBook()
    {
        // Arrange
        var existingBook = await _repository.GetAsync(1);

        // Act
        var expression = existingBook != null;

        // Assert
        Assert.True(expression);
    }

    /// <summary>
    /// Tests if a category is successfully added to a book.
    /// </summary>
    [Fact]
    public void AddCategory_Book_CategoryIsAdded()
    {
        // Arrange
        Book book = EntityFactoryHelper.CreateBook("test", 1, 1);
        var categoriesCount = book.Categories.Count;
        Category category = EntityFactoryHelper.CreateCategory("test");

        // Act
        book.AddCategory(category);

        // Assert
        Assert.Equal(categoriesCount + 1, book.Categories.Count);
    }

    /// <summary>
    /// Tests if a category is successfully removed from a book.
    /// </summary>
    [Fact]
    public void RemoveCategory_Book_CategoryIsRemoved()
    {
        // Arrange
        Book book = EntityFactoryHelper.CreateBook("test", 1, 1);
        Category category = EntityFactoryHelper.CreateCategory("test");
        book.AddCategory(category);
        var categoriesCount = book.Categories.Count;

        // Act
        book.RemoveCategory(category);

        // Assert
        Assert.Equal(categoriesCount - 1, book.Categories.Count);
    }
}
