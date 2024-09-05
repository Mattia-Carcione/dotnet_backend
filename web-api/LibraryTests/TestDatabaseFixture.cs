using Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryTests;

/// <summary>
/// A database fixture for testing database in production.
/// </summary>
public class TestDatabaseFixture
{
    /// <summary>
    /// A conncetion string connecting with the database.
    /// </summary>
    private const string ConnectionString =
        @"Server=(localdb)\mssqllocaldb;Database=EFTestSample;Trusted_Connection=True;ConnectRetryCount=0";

    /// <summary>
    /// A object lock.
    /// </summary>
    private static readonly object _lock = new();

    /// <summary>
    /// A bool to indicate if a database was initialized.
    /// </summary>
    private static bool _databaseInitialized;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestDatabaseFixture"/> class.
    /// Ensures that the database is deleted and created afresh if it hasn't been initialized yet.
    /// </summary>
    public TestDatabaseFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }

                _databaseInitialized = true;
            }
        }
    }

    /// <summary>
    /// Creates a new instance of <see cref="LibraryContext"/> with the configured options.
    /// </summary>
    /// <returns>A new <see cref="LibraryContext"/> instance.</returns>
    public LibraryContext CreateContext() =>
        new LibraryContext(
            new DbContextOptionsBuilder<LibraryContext>().UseSqlServer(ConnectionString).Options
        );
}
