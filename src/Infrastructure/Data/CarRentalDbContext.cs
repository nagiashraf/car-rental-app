using System.Reflection;

namespace Infrastructure.Data;

/// <summary>
/// Represents the database context for the Car Rental application.
/// </summary>
public class CarRentalDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CarRentalDbContext"/> class.
    /// </summary>
    /// <param name="options">The options for configuring the context.</param>
    public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
    : base(options)
    {
    }

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> of <see cref="Language"/> entity.
    /// </summary>
    public DbSet<Language> Languages => this.Set<Language>();

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> of <see cref="LocationBase"/> entity.
    /// </summary>
    public DbSet<LocationBase> LocationsBases => this.Set<LocationBase>();

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> of <see cref="LocationsTranslations"/> entity.
    /// </summary>
    public DbSet<LocationTranslation> LocationsTranslations => this.Set<LocationTranslation>();

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> of <see cref="CurrencyBase"/> entity.
    /// </summary>
    public DbSet<CurrencyBase> CurrenciesBases => this.Set<CurrencyBase>();

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> of <see cref="CurrencyTranslation"/> entity.
    /// </summary>
    public DbSet<CurrencyTranslation> CurrenciesTranslations => this.Set<CurrencyTranslation>();

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> of <see cref="AllowedLocation"/> entity.
    /// </summary>
    public DbSet<AllowedLocation> AllowedLocations => this.Set<AllowedLocation>();

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types exposed in <see cref="DbSet{TEntity}"/> properties on this context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}