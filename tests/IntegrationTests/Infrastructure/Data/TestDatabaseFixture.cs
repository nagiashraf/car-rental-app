#pragma warning disable CA1822

using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace Tests.Data.Tests;

public class TestDatabaseFixture
{
    private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=CarRentalDB_Tests;Trusted_Connection=True";
    private static readonly object LockObject = new ();
    private static bool databaseIsInitialized;

    public TestDatabaseFixture() => this.InitializeDatabase();

    public CarRentalDbContext CreateContext()
        => new (
            new DbContextOptionsBuilder<CarRentalDbContext>()
                .UseSqlServer(ConnectionString)
                .Options);

    private void InitializeDatabase()
    {
        lock (LockObject)
        {
            if (databaseIsInitialized)
            {
                return;
            }

            using var context = this.CreateContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            new CarRentalContextSeed(context, new NullLogger<CarRentalContextSeed>(), this.GetProjectDirectory()).Seed();

            databaseIsInitialized = true;
        }
    }

    private string GetProjectDirectory()
        => Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName
        ?? throw new InvalidOperationException("The project directory could not be found.");
}