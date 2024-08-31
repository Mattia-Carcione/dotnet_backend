using System.ComponentModel.DataAnnotations;

namespace DTOs.BookDTOs;

/// <summary>
/// Represents DTO of the book.
/// </summary>
public class BookDTO
{
    /// <summary>
    /// Gets or sets the unique identifier of the book.
    /// </summary>
    /// <value>
    /// The unique identifier of the book
    /// </value>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the book.
    /// </summary>
    /// <value>
    /// The title of the book.
    /// </value>
    /// <remarks>
    /// The field is required. Min legth: 3, max length: 50.
    /// </remarks>
    [Required]
    [MaxLength(50), MinLength(3)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of the pages in the book.
    /// </summary>
    /// <value>
    /// The number of the pages in the book.
    /// </value>
    public int Pages { get; set; }

    /// <summary>
    /// Gets or sets the total number of copies available.
    /// </summary>
    /// <value>
    /// The total number of copies available.
    /// </value>
    public int TotalCopies { get; set; }

    /// <summary>
    /// Gets or sets the number of copies currently avalaible.
    /// </summary>
    /// <value>
    /// The number of copies currently avalaible.
    /// </value>
    public int Copies { get; set; }

    /// <summary>
    /// Gets or sets the publication date of the book.
    /// </summary>
    /// <value>
    /// The publication date of the book.
    /// </value>
    public DateTime PublicationDate { get; set; }
}
