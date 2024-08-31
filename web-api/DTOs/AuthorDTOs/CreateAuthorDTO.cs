using System.ComponentModel.DataAnnotations;

namespace DTOs.AuthorDTOs;

/// <summary>
/// Represents DTO for creating new Author.
/// </summary>
public class CreateAuthorDTO
{
    /// <summary>
    /// Gets or sets the name of the author.
    /// </summary>
    /// <value>
    /// the name of the author.
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
    /// the last name of the author.
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
}
