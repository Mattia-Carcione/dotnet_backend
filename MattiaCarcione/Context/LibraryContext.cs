//TODO:
// Creare la struttura di tabelle sul database

using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Context;

public class LibraryContext(DbContextOptions<LibraryContext> options) : DbContext(options)
{
    public DbSet<Author> Authors {get; set;}
    public DbSet<Book> Books {get; set;}
    public DbSet<Category> Categories {get; set;}
    public DbSet<Booking> Bookings {get; set;}
    public DbSet<Editor> Editors {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            {
                throw new NullReferenceException();
            }
    }
}
