using LibraryModel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryContext
{
    public class LibraryDBContext : DbContext
    {
        internal static IConfiguration? Config { get; private set; }
        public LibraryDBContext() { }
        public LibraryDBContext(DbContextOptions<LibraryDBContext> options) : base(options) { }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

                string? _connectionString = Config.GetConnectionString("DefaultConnection");

                if (_connectionString != null) optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}
