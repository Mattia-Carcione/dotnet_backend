using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
*TODO:
*Libro
*+Titolo: string
*+NumeroPagine : int 
*+DataPubblicazione : Date 
*+NumeroCopieTotali : int 
*+NumeroCopieRimaste : int
*
*+AddCategoria(in c : Categoria) 
*+RemoveCategoria(in c : Categoria) 
*/
namespace Models.Entities;

/// <summary>
/// Represents the <see cref="Book"/> entity.
/// </summary>
[Table("Books")]
public class Book
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
    /// the title of the book.
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
    /// Adds the <see cref="Category"/> to the entity <see cref="Book"/>.
    /// </summary>
    /// <param name="category">The <see cref="Category"/> entity to add.</param>
    public void AddCategory(Category category) 
    {
        if(Categories != null && !Categories.Contains(category))
        {
            Categories.Add(category);
        }
    }

    /// <summary>
    /// Removes the <see cref="Category"/> from the <see cref="Book"/>.
    /// </summary>
    /// <param name="category">The <see cref="Category"/> entity to remove.</param>
    public void RemoveCategory(Category category) 
    {
        if(Categories != null && Categories.Contains(category))
        {
            Categories.Remove(category);
        }
    }

    /// <summary>
    /// Gets or sets the collection of the categories associated with the book.
    /// </summary>
    /// <value>
    /// The book collection of the categories associated with the book.
    /// </value>
    /// <remarks>
    /// This property represents the relationship between the book and the <see cref="Category"/> entity.
    /// </remarks>
    public ICollection<Category> Categories {get; set;} = new List<Category>();

    /// <summary>
    /// Gets or sets the collection of the bookings associated with the book.
    /// </summary>
    /// <value>
    /// The book collection of the bookings associated with the book.
    /// </value>
    /// <remarks>
    /// This property represents the relationship between the book and the <see cref="Booking"/> entity.
    /// </remarks>
    public ICollection<Booking> Bookings {get; set;} = new List<Booking>();

    /// <summary>
    /// Gets or sets the collection of the book associated with the book.
    /// </summary>
    /// <value>
    /// The id of the atuthor associated with the book.
    /// </value>
    /// <remarks>
    /// This property represents the relationship between the book and the <see cref="Author"/> entity.
    /// </remarks>
    [ForeignKey("Author")]
    [Required]
    public int AuthorId { get; set; }

    /// <summary>
    /// Gets or sets the author associated with the book.
    /// </summary>
    /// <value>
    /// The author of the book associated with the book.
    /// </value>
    /// <remarks>
    /// This property represents the relationship between the book and the <see cref="Author"/> entity.
    /// </remarks>
    public Author? Author { get; set; }

    /// <summary>
    /// Gets or sets the id of editor associated with the book.
    /// </summary>
    /// <value>
    /// The id of the editor associated with the book.
    /// </value>
    /// <remarks>
    /// This property represents the relationship between the book and the <see cref="Editor"/> entity.
    /// </remarks>
    [ForeignKey("Editor")]
    [Required]
    public int EditorId { get; set; }

    /// <summary>
    /// Gets or sets the collection of the book associated with the book.
    /// </summary>
    /// <value>
    /// The book collection of the book associated with the book.
    /// </value>
    /// <remarks>
    /// This property represents the relationship between the book and the <see cref="Editor"/> entity.
    /// </remarks>
    public Editor? Editor {get; set;}

    /// <summary>
    /// Represents the relationship between <see cref="Book"/> and <see cref="Order"/>.
    /// </summary>
    /// <value>
    /// The order collection of the book.
    /// </value>
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
