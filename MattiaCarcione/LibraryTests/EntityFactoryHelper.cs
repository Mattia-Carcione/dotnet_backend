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
}
