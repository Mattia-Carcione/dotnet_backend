/**
*TODO:
*a. Test sui metodi CRUD degli elementi, in particolare del libro.
*
*b. Test di salvataggio di un libro contenente diverse categorie. Aggiungere e
*togliere categorie al libro e verificare che il salvataggio funzioni
*correttamente.
**/

using Context;
using Models.Entities;
using Repository;

namespace LibraryTests;

public class GenericRepositoryTest : IClassFixture<TestDatabaseFixture>
{
    public GenericRepository<Book, LibraryContext> _repository { get; private set; }
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
        var (initialList, metadata) = await _repository.GetAllAsync(1, 10, c => c.OrderBy(b => b.Id));
        var initialCount = initialList.Count();
        Book book = EntityFactoryHelper.CreateBook("test", 1, 1);

        //Act
        await _repository.AddAsync(book);
        await _repository.SaveChangesAsync();
        var (finalList, metadata2) = await _repository.GetAllAsync(1, 10, c => c.OrderBy(b => b.Id));
        var FinalCount = finalList.Count();


        //Assert
        Assert.Equal(initialCount + 1, FinalCount);
    }

    [Fact]
    public async Task UpdateBook_Book_BookIsUpdated()
    {
        //Assign
        Book book = await _repository.GetAsync(1) ?? throw new ArgumentNullException();
        var initialCopies = book.TotalCopies;
        book.TotalCopies++;

        //Act
        _repository.Update(book);
        await _repository.SaveChangesAsync();
        var updatedBook = await _repository.GetAsync(book.Id) ?? throw new ArgumentNullException();

        //Assert
        Assert.Equal(initialCopies + 1, updatedBook.TotalCopies);
    }

    [Fact]
    public async Task DeleteBook_Book_BookIsRemoved()
    {
        //Assign
        var (initialList, metadata) = await _repository.GetAllAsync(1, 10, c => c.OrderBy(b => b.Id));
        var count = initialList.Count();
        Book book = await _repository.GetAsync(1) ?? throw new ArgumentNullException();

        //Act
        _repository.Delete(book);
        await _repository.SaveChangesAsync();
        var (finalList, metadata2) = await _repository.GetAllAsync(1, 10, c => c.OrderBy(b => b.Id));

        //Assert
        Assert.Equal(count - 1, finalList.Count());
    }

    [Fact]
    public async Task GetAllBook_Book_GetListOFBook()
    {
        //Assign
        var (initialList, metadata) = await _repository.GetAllAsync(1, 10, c => c.OrderBy(b => b.Id));

        //Act
        var count = initialList.Count();

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
