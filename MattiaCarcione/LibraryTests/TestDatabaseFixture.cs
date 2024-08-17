using Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryTests;

public class TestDatabaseFixture : EntityFactoryHelper
{
    private const string ConnectionString =
        @"Server=(localdb)\mssqllocaldb;Database=EFTestSample;Trusted_Connection=True;ConnectRetryCount=0";

    private static readonly object _lock = new();
    private static bool _databaseInitialized;

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

    public LibraryContext CreateContext() =>
        new LibraryContext(
            new DbContextOptionsBuilder<LibraryContext>().UseSqlServer(ConnectionString).Options
        );
}
