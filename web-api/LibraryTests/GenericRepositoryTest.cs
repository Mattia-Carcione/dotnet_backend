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
        var initialList = await _repository.GetAllAsync();
        Book book = EntityFactoryHelper.CreateBook("test", 1, 1);

        //Act
        await _repository.AddAsync(book);
        await _repository.SaveChangesAsync();
        var finalList = await _repository.GetAllAsync();

        //Assert
        Assert.Equal(initialList.Count() + 1, finalList.Count());
    }

    [Fact]
    public async Task UpdateBook_Book_BookIsUpdated()
    {
        //Assign
        Book book = await _repository.GetAsync(1);
        book.TotalCopies++;
        var initialCopies = book.TotalCopies;

        //Act
        _repository.Update(book);
        await _repository.SaveChangesAsync();
        var updatedBook = await _repository.GetAsync(book.Id);

        //Assert
        Assert.Equal(initialCopies, updatedBook.TotalCopies);
    }

    [Fact]
    public async Task DeleteBook_Book_BookIsRemoved()
    {
        //Assign
        var initialList = await _repository.GetAllAsync();
        var count = initialList.Count();
        Book book = await _repository.GetAsync(1);

        //Act
        _repository.Delete(book);
        await _repository.SaveChangesAsync();
        var finalList = await _repository.GetAllAsync();

        //Assert
        Assert.Equal(count - 1, finalList.Count());
    }

    [Fact]
    public async Task GetAllBook_Book_GetListOFBook()
    {
        //Assign
        var allBooks = await _repository.GetAllAsync();

        //Act
        var count = allBooks.Count();

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
        //Assert
        Book book = EntityFactoryHelper.CreateBook("test", 1, 1);
        var categoriesCount = book.Categories.Count;
        Category category = EntityFactoryHelper.CreateCategory("test");

        //Act
        book.AddCategory(category);

        //Assert
        Assert.Equal(categoriesCount + 1, book.Categories.Count);
    }

    [Fact]
    public void RemoveCategory_Book_CategoryIsRemoved()
    {
        //Assign
        Book book = EntityFactoryHelper.CreateBook("test", 1, 1);
        Category category = EntityFactoryHelper.CreateCategory("test");
        book.AddCategory(category);
        var categoriesCount = book.Categories.Count;

        //Act
        book.RemoveCategory(category);

        //Assert
        Assert.Equal(categoriesCount - 1, book.Categories.Count);

    }
}
