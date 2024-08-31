using System.ComponentModel.DataAnnotations;

namespace DTOs.CategoryDTOs;

/// <summary>
/// Represents DTO for the category.
/// </summary>
public class CategoryDTO
{
    /// <summary>
    /// Gets or sets the unique identifier of the category.
    /// </summary>
    /// <value>
    /// The unique identifier of the category
    /// </value>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the genre of the category.
    /// </summary>
    /// <value>
    /// The genre of the category.
    /// </value>
    /// <remarks>
    /// The field is required. Min legth: 3, max length: 50.
    /// </remarks>
    [Required]
    [MaxLength(50), MinLength(3)]
    public string Genre { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the category.
    /// </summary>
    /// <value>
    /// The description of the category.
    /// </value>
    /// <remarks>
    /// Min legth: 3, max length: 400.
    /// </remarks>
    [MaxLength(400), MinLength(3)]
    public string Description { get; set; } = string.Empty;
}
