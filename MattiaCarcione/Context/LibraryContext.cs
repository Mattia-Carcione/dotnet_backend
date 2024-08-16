//TODO:
// Creare la struttura di tabelle sul database

using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Context;

public class LibraryContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Editor> Editors { get; set; }

    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     if (!optionsBuilder.IsConfigured)
    //     {
    //         throw new NullReferenceException();
    //     }
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
            .WithMany(b => b.Books);
    }
}
