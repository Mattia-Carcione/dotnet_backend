using Model.Entities;

namespace LibraryTests;

public class EntityFactoryHelper
{
    public Book CreateBook(string title, int authorId, int editorId, int pages = 0, int totalCopies = 0, int copies = 0, DateTime? publicationDate = null)
    {
        return new Book
        {
            Title = title,
            AuthorId = authorId,
            EditorId = editorId,
            Pages = pages,
            TotalCopies = totalCopies,
            Copies = copies,
            PublicationDate = publicationDate ?? DateTime.Today
        };
    }

    public Category CreateCategory(string genre, string description = "")
    {
        return new Category { Genre = genre, Description = description };
    }

    public Booking CreateBooking(string user, int bookId)
    {
        return new Booking { User = user, BookId = bookId, BookingDate = DateTime.Now };
    }

    public Author CreateAuthor(string name, string lastName)
    {
        return new Author { Name = name, LastName = lastName, BirthDate = DateTime.Now };
    }

    public Editor CreateEditor(string name)
    {
        return new Editor { Name = name };
    }
}
