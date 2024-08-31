using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
*TODO:
*Autore
*+Nome: string
*+Cognome : int 
*+DataNascita : Date
*/
namespace Models.Entities;

/// <summary>
/// Represents the author entity of the context.
/// </summary>
[Table("Authors")]
public class Author
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
    /// <para>
    /// Min legth: 3, max length: 50.
    /// </para>
    /// The field is required.
    /// </remarks>
    [Required]
    [MinLength(3), MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when creating new author.
    /// </summary>
    /// <remarks>
    /// This date is typically set to the current date when creating a new author.
    /// </remarks>
    /// <value>
    /// The birth date of the author.
    /// </value>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Gets or sets the collection of the book associated with the author.
    /// </summary>
    /// <value>
    /// The book collection of the book associated with the author.
    /// </value>
    /// <remarks>
    /// This property represents the relationship between the author and the <see cref="Book"/> entity.
    /// </remarks>
    public ICollection<Book> Books {get; set;} = new List<Book>();
}
