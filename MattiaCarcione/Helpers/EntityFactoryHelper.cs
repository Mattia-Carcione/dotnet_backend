/*
*TODO:
*Creare un helper che crea le entità
*/

using Model.Entities;

namespace LibraryTests;

public static class EntityFactoryHelper
{
    public static Book CreateBook(string title, int authorId, int editorId, int pages = 0, int totalCopies = 0, int copies = 0, DateTime? publicationDate = null)
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

    public static Category CreateCategory(string genre, string description = "")
    {
        return new Category { Genre = genre, Description = description };
    }

    public static Booking CreateBooking(string user, Book book, DateTime? deliveryDate = null)
    {
        return new Booking { User = user, Book = book, BookingDate = DateTime.Today, DeliveryDate = deliveryDate ?? default };
    }
}
