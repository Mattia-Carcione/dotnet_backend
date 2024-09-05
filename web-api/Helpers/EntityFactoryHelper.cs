/*
*TODO:
*Creare un helper che crea le entità
*/

using Models.Entities;

namespace LibraryTests;

/// <summary>
/// Provides helper methods to create instances of various entities.
/// </summary>
public static class EntityFactoryHelper
{
    /// <summary>
    /// Creates a new instance of <see cref="Book"/>.
    /// </summary>
    /// <param name="title">The title of the book.</param>
    /// <param name="authorId">The author id.</param>
    /// <param name="editorId">The editor id.</param>
    /// <param name="pages">The number of pages in the book.</param>
    /// <param name="totalCopies">The total number of copies available.</param>
    /// <param name="copies">The number of copies currently avalaible.</param>
    /// <param name="publicationDate">The publication date of the book.</param>
    /// <returns>A new instance of <see cref="Book"/>.</returns>
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

    /// <summary>
    /// Creates a new instance of <see cref="Category"/>.
    /// </summary>
    /// <param name="genre">The genre of the category.</param>
    /// <param name="description">The description of the category.</param>
    /// <returns>A new instance of <see cref="Category"/>.</returns>
    public static Category CreateCategory(string genre, string description = "")
    {
        return new Category { Genre = genre, Description = description };
    }

    /// <summary>
    /// Creates a new instance of <see cref="Booking"/>.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="book">The <see cref="Book"/> being booked.</param>
    /// <param name="returnDate">The return date.</param>
    /// <returns>A new instance of <see cref="Booking"/>.</returns>
    public static Booking CreateBooking(User user, Book book, DateTime? returnDate = null)
    {
        return new Booking { User = user, Book = book, BookingDate = DateTime.Now, ReturnDate = returnDate ?? default };
    }

    /// <summary>
    /// Creates a new instance of <see cref="User"/>.
    /// </summary>
    /// <param name="username">The name of the user.</param>
    /// <param name="email">The email of the user.</param>
    /// <param name="isPremium">The role of the user.</param>
    /// <returns>A new instance of <see cref="User"/>.</returns>
    public static User CreateUser(string username, string email, bool isPremium = false)
    {
        return new User { Username = username, Email = email, IsPremium = isPremium };
    }

    public static Order CreateOrder(int userId, int bookId)
    {
        return new Order { BookId = bookId, UserId = userId };
    }
}
