using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Entities.Model;

namespace Entities.Data
{
    public class Library_DbContext : DbContext
    {
        public Library_DbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


    }
    public class LibraryContextFactory : IDesignTimeDbContextFactory<Library_DbContext>
    {

        public Library_DbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"Connection String: {connectionString}");  // Debug print

            var optionsBuilder = new DbContextOptionsBuilder<Library_DbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new Library_DbContext(optionsBuilder.Options);
        }

    }


}


