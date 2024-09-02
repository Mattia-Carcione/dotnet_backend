//TODO:
// Creare la struttura di tabelle sul database

using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Context;

/// <summary>
/// A <see cref="LibraryContext"/> instance representing a session with the database and can be used to query and save instances of the entities.
/// 
/// <remarks>
/// <para>
/// Defines enitity relationship and seed data.
/// </para>
/// </remarks>
/// </summary>
public class LibraryContext : DbContext
{
    /// <summary>
    /// Gets or sets of the <see cref="DbSet{TEntity}"/> of the <see cref="Author"/> entity.
    /// </summary>
    public DbSet<Author> Authors { get; set; }

    /// <summary>
    /// Gets or sets of the <see cref="DbSet{TEntity}"/> of the <see cref="Book"/> entity.
    /// </summary>
    public DbSet<Book> Books { get; set; }

    /// <summary>
    /// Gets or sets of the <see cref="DbSet{TEntity}"/> of the <see cref="Category"/> entity.
    /// </summary>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Gets or sets of the <see cref="DbSet{TEntity}"/> of the <see cref="Booking"/> entity.
    /// </summary>
    public DbSet<Booking> Bookings { get; set; }

    /// <summary>
    /// Gets or sets of the <see cref="DbSet{TEntity}"/> of the <see cref="Editor"/> entity.
    /// </summary>
    public DbSet<Editor> Editors { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LibraryContext"/> using the specified <paramref name="options"/> of <see cref="DbContextOptions{TContext}"/>.
    /// </summary>
    /// <param name="options">The options to be used by a db context.</param>
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }

    /// <summary>
    /// Configures a context by overriding <see cref="DbContext.OnConfiguring(DbContextOptionsBuilder)"/>.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for this context.</param>
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     if (!optionsBuilder.IsConfigured)
    //     {
    //        //your code here
    //     }
    // }

    /// <summary>
    /// Configures the model and relationships between entities, and seeds initial data into the database using <see cref="ModelBuilder"/>.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seeding Authors
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "Jane", LastName = "Austen", BirthDate = new DateTime(1775, 12, 16) },
            new Author { Id = 2, Name = "Mark", LastName = "Twain", BirthDate = new DateTime(1835, 11, 30) },
            new Author { Id = 3, Name = "Charles", LastName = "Dickens", BirthDate = new DateTime(1812, 2, 7) },
            new Author { Id = 4, Name = "Mary", LastName = "Shelley", BirthDate = new DateTime(1797, 8, 30) },
            new Author { Id = 5, Name = "George", LastName = "Orwell", BirthDate = new DateTime(1903, 6, 25) }
        );

        // Seeding Editors
        modelBuilder.Entity<Editor>().HasData(
            new Editor { Id = 1, Name = "Penguin Books" },
            new Editor { Id = 2, Name = "HarperCollins" },
            new Editor { Id = 3, Name = "Random House" },
            new Editor { Id = 4, Name = "Simon & Schuster" },
            new Editor { Id = 5, Name = "Macmillan Publishers" }
        );

        // Seeding Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Genre = "Fiction", Description = "Fictional books" },
            new Category { Id = 2, Genre = "Science Fiction", Description = "Sci-Fi books" },
            new Category { Id = 3, Genre = "Mystery", Description = "Mystery and detective stories" },
            new Category { Id = 4, Genre = "Biography", Description = "Biographical works" },
            new Category { Id = 5, Genre = "Horror", Description = "Horror and thriller books" }
        );

        // Seeding Books
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Pride and Prejudice", Pages = 432, TotalCopies = 10, Copies = 5, PublicationDate = new DateTime(1813, 1, 28), AuthorId = 1, EditorId = 1 },
            new Book { Id = 2, Title = "Adventures of Huckleberry Finn", Pages = 366, TotalCopies = 15, Copies = 7, PublicationDate = new DateTime(1884, 12, 10), AuthorId = 2, EditorId = 2 },
            new Book { Id = 3, Title = "Great Expectations", Pages = 544, TotalCopies = 12, Copies = 8, PublicationDate = new DateTime(1861, 1, 1), AuthorId = 3, EditorId = 3 },
            new Book { Id = 4, Title = "Frankenstein", Pages = 280, TotalCopies = 8, Copies = 3, PublicationDate = new DateTime(1818, 1, 1), AuthorId = 4, EditorId = 4 },
            new Book { Id = 5, Title = "1984", Pages = 328, TotalCopies = 20, Copies = 10, PublicationDate = new DateTime(1949, 6, 8), AuthorId = 5, EditorId = 5 },
            new Book { Id = 6, Title = "Emma", Pages = 328, TotalCopies = 20, Copies = 0, PublicationDate = new DateTime(1815, 6, 8), AuthorId = 1, EditorId = 1 }
        );

        // Seeding Bookings
        modelBuilder.Entity<Booking>().HasData(
            new Booking { Id = 1, User = "User1", BookingDate = DateTime.Now.AddDays(-5), BookId = 1 }, // No ReturnDate
            new Booking { Id = 2, User = "User1", BookingDate = DateTime.Now.AddDays(-10), BookId = 2 }, // No ReturnDate
            new Booking { Id = 3, User = "User1", BookingDate = DateTime.Now.AddDays(-15), BookId = 3 }, // No ReturnDate
            new Booking { Id = 4, User = "User2", BookingDate = DateTime.Now.AddDays(-7), BookId = 4 }, // No ReturnDate
            new Booking { Id = 5, User = "User3", BookingDate = DateTime.Now.AddDays(-20), ReturnDate = DateTime.Now.AddDays(-10), BookId = 5 } // With ReturnDate
        );

        // Defines relationship between the entities
        modelBuilder
            .Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(b => b.Books)
            .HasForeignKey(b => b.AuthorId);

        modelBuilder
            .Entity<Book>()
            .HasOne(b => b.Editor)
            .WithMany(b => b.Books)
            .HasForeignKey(b => b.EditorId);

        modelBuilder
            .Entity<Book>()
            .HasMany(b => b.Bookings)
            .WithOne(b => b.Book)
            .HasForeignKey(b => b.BookId);

        modelBuilder
            .Entity<Book>()
            .HasMany(b => b.Categories)
            .WithMany(c => c.Books)
            .UsingEntity(e => e.HasData(
                new { BooksId = 1, CategoriesId = 1, },
                new { BooksId = 2, CategoriesId = 2 },
                new { BooksId = 3, CategoriesId = 3 },
                new { BooksId = 4, CategoriesId = 4 },
                new { BooksId = 5, CategoriesId = 5 }
            ));
    }
}
