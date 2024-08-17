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
    public GenericRepository<Book> _repository { get; private set; }
    public TestDatabaseFixture Fixture { get; }

    public GenericRepositoryTest(TestDatabaseFixture fixture)
    {
        Fixture = fixture;

        var context = Fixture.CreateContext();

        _repository = new(context);
    }

    [Fact]
    public async Task AddBook_Book_BookIsAdded()
    {
        //Assign
        var allBooks = await _repository.GetAllAsync();
        Book book = Fixture.CreateBook("test", 1, 1);

        //Act
        await _repository.AddAsync(book);
        await _repository.SaveChangesAsync();
        var newAllBooks = await _repository.GetAllAsync();

        //Assert
        Assert.Equal(allBooks.Count + 1, newAllBooks.Count);
    }

    [Fact]
    public async Task UpdateBook_Book_BookIsUpdated()
    {
        //Assign
        Book book = Fixture.CreateBook("test", 1, 1);

        await _repository.AddAsync(book);
        await _repository.SaveChangesAsync();

        book.TotalCopies++;

        //Act
        _repository.Update(book);
        await _repository.SaveChangesAsync();
        var updatedBook = await _repository.GetAsync(book.Id);

        //Assert
        Assert.Equal(book.TotalCopies, updatedBook.TotalCopies);
    }

    [Fact]
    public async Task DeleteBook_Book_BookIsRemoved()
    {
        //Assign
        Book book = Fixture.CreateBook("test", 1, 1);

        await _repository.AddAsync(book);
        await _repository.SaveChangesAsync();
        var allBooks = await _repository.GetAllAsync();
        var existingBook = allBooks.FirstOrDefault(b => b.Title == book.Title);

        //Act
        if (existingBook is not null)
            _repository.Delete(existingBook);
        await _repository.SaveChangesAsync();
        var newAllBooks = await _repository.GetAllAsync();

        //Assert
        Assert.Equal(allBooks.Count - 1, newAllBooks.Count);
    }

    [Fact]
    public async Task GetAllBook_Book_GetListOFBook()
    {
        //Assign
        var allBooks = await _repository.GetAllAsync();

        //Act
        var count = allBooks.Count;

        //Assert
        Assert.True(count > 0);
    }

    [Fact]
    public async Task GetBook_Book_GetBook()
    {
        //Assign
        var existingBook = await _repository.GetAsync(1);

        //Act
        var expression = existingBook != null;

        //Assert
        Assert.True(expression);
    }

    [Fact]
    public void AddCategory_Book_CategoryIsAdded()
    {
        //Act
        Book book = Fixture.CreateBook("test", 1, 1);
        Category category = Fixture.CreateCategory("test");
        book.AddCategory(category);

        //Assert
        Assert.True(book.Categories?.Count == 1);
    }

    [Fact]
    public void RemoveCategory_Book_CategoryIsRemoved()
    {
        //Assign
        Book book = Fixture.CreateBook("test", 1, 1);
        Category category = Fixture.CreateCategory("test");
        book.AddCategory(category);

        //Act
        book.RemoveCategory(category);

        //Assert
        Assert.True(book.Categories.Count == 0);
    }
}
