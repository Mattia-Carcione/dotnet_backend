/**
*TODO:
*a. Test sui metodi CRUD degli elementi, in particolare del libro.
*
*b. Test di salvataggio di un libro contenente diverse categorie. Aggiungere e
*togliere categorie al libro e verificare che il salvataggio funzioni
*correttamente.
**/

using Model.Entities;
using Repository;

namespace LibraryTests;

public class GenericRepositoryTest : IClassFixture<TestDatabaseFixture>
{
    public GenericRepository<Book> _context { get; private set; }
    public TestDatabaseFixture Fixture { get; }

    public GenericRepositoryTest(TestDatabaseFixture fixture)
    {
        Fixture = fixture;

        var context = Fixture.CreateContext();

        _context = new(context);
    }

    private Book CreateBook()
    {
        return new Book
        {
            Title = "newBook",
            AuthorId = 1,
            EditorId = 1
        };
    }

    private Category CreateCategory()
    {
        return new Category { Genre = "Test" };
    }

    [Fact]
    public async Task AddBook_Book_BookIsAdded()
    {
        //Assign
        var allBooks = await _context.GetAllAsync();
        Book book = CreateBook();

        //Act
        await _context.AddAsync(book);
        await _context.SaveChangesAsync();
        var newAllBooks = await _context.GetAllAsync();

        //Assert
        Assert.Equal(allBooks.Count + 1, newAllBooks.Count);
    }

    [Fact]
    public async Task UpdateBook_Book_BookIsUpdated()
    {
        //Assign
        Book book = CreateBook();

        await _context.AddAsync(book);
        await _context.SaveChangesAsync();

        book.TotalCopies++;

        //Act
        _context.Update(book);
        await _context.SaveChangesAsync();
        var updatedBook = await _context.GetAsync(book.ID);

        //Assert
        Assert.Equal(book.TotalCopies, updatedBook.TotalCopies);
    }

    [Fact]
    public async Task DeleteBook_Book_BookIsRemoved()
    {
        //Assign
        Book book = CreateBook();

        await _context.AddAsync(book);
        await _context.SaveChangesAsync();
        var allBooks = await _context.GetAllAsync();
        var existingBook = allBooks.FirstOrDefault(b => b.Title == book.Title);

        //Act
        if (existingBook is not null)
            _context.Delete(existingBook);
        await _context.SaveChangesAsync();
        var newAllBooks = await _context.GetAllAsync();

        //Assert
        Assert.Equal(allBooks.Count - 1, newAllBooks.Count);
    }

    [Fact]
    public async Task GetAllBook_Book_GetListOFBook()
    {
        //Assign
        var allBooks = await _context.GetAllAsync();

        //Act
        var count = allBooks.Count;

        //Assert
        Assert.True(count > 0);
    }

    [Fact]
    public async Task GetBook_Book_GetBook()
    {
        //Assign
        var existingBook = await _context.GetAsync(1);

        //Act
        var expression = existingBook != null;

        //Assert
        Assert.True(expression);
    }

    [Fact]
    public void AddCategory_Book_CategoryIsAdded()
    {
        //Act
        Book book = CreateBook();
        Category category = CreateCategory();
        book.AddCategory(category);

        //Assert
        Assert.True(book.Categories?.Count == 1);
    }

    [Fact]
    public void RemoveCategory_Book_CategoryIsRemoved()
    {
        //Assign
        Book book = CreateBook();
        Category category = CreateCategory();
        book.AddCategory(category);

        //Act
        book.RemoveCategory(category);

        //Assert
        Assert.True(book.Categories.Count == 0);
    }
}
