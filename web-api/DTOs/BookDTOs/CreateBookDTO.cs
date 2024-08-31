using System.ComponentModel.DataAnnotations;

namespace DTOs.BookDTOs;

/// <summary>
/// Represents DTO for creating new book.
/// </summary>
public class CreateBookDTO
{
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

    /// <summary>
    /// Gets or sets the id of the author associated of the book.
    /// </summary>
    /// <value>
    /// The author id of the book.
    /// </value>
    /// <remarks>
    /// <para>
    /// This property represents the relationship between the book and the author.
    /// </para>
    /// <para>
    /// The field is required.
    /// </para>
    /// </remarks>
    [Required]
    public int AuthorId { get; set; }

    /// <summary>
    /// Gets or sets the id of the editor associated of the book.
    /// </summary>
    /// <value>
    /// The editor id of the book.
    /// </value>
    /// <remarks>
    /// <para>
    /// This property represents the relationship between the book and the editor.
    /// </para>
    /// <para>
    /// The field is required.
    /// </para>
    /// </remarks>
    [Required]
    public int EditorId { get; set; }
}
