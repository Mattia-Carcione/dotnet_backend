using System.ComponentModel.DataAnnotations;
using DTOs.BookDTOs;

namespace DTOs.AuthorDTOs;

/// <summary>
/// Represents the DTO of the Author.
/// </summary>
public class AuthorDTO
{
    /// <summary>
    /// Gets or sets the unique identifier of the author.
    /// </summary>
    /// <value>
    /// The unique identifier of the author
    /// </value>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the author.
    /// </summary>
    /// <value>
    /// The name of the author.
    /// </value>
    /// <remarks>
    /// The field is required. Min legth: 3, max length: 50.
    /// </remarks>
    [Required]
    [MinLength(3), MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the author.
    /// </summary>
    /// <value>
    /// The last name of the author.
    /// </value>
    /// <remarks>
    /// The field is required. Min legth: 3, max length: 50.
    /// </remarks>
    [Required]
    [MinLength(3), MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the birth date of author.
    /// </summary>
    /// <value>
    /// The birth date of the author.
    /// </value>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Gets or sets the collection of the books associated with the author.
    /// </summary>
    /// <value>
    /// The collection of the books associated with the author.
    /// </value>
    /// <remarks>
    /// This property represents the relationship between the author and the <see cref="BookDTO"/> entity.
    /// </remarks>
    public ICollection<BookDTO> Books { get; set; } = new List<BookDTO>();
}
