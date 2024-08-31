using System.ComponentModel.DataAnnotations;
using DTOs.CategoryDTOs;

namespace DTOs.BookDTOs;

/// <summary>
/// Represents DTO for the detail of the book.
/// </summary>
public class BookDetailDTO
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

    /// <summary>
    /// Gets or sets the name and last name of the author associated with the book.
    /// </summary>
    /// <value>
    /// The name and last name of the book associated with the book.
    /// </value>
    /// <remarks>
    /// This property represents the relationship between the book and the <see cref="Author"/> entity.
    /// </remarks>
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the editor associated with the book.
    /// </summary>
    /// <value>
    /// The name of the editor associated with the book.
    /// </value>
    /// <remarks>
    /// This property represents the relationship between the book and the <see cref="Editor"/> entity.
    /// </remarks>
    public string Editor { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection of the categories associated with the book.
    /// </summary>
    /// <value>
    /// The collection of the categories associated with the book.
    /// </value>
    /// <remarks>
    /// This property represents the relationship between the book and the <see cref="CategoryDTO"/> entity.
    /// </remarks>
    public ICollection<CategoryDTO> Categories{ get; set; } = new List<CategoryDTO>();
}
