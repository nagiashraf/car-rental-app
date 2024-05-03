using Core.DTOs;

namespace Infrastructure.Data.Repositories;

/// <summary>
/// Repository class for handling CRUD operations related to the 'Location' entity.
/// Implements the GenericRepository for common repository functionality and the ILocationRepository interface.
/// </summary>
public class LocationRepository : ILocationRepository
{
    private readonly CarRentalDbContext dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocationRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context used for interacting with the underlying database.</param>
    public LocationRepository(CarRentalDbContext dbContext) => this.dbContext = dbContext;

    /// <summary>
    /// Retrieves a location by its GUID asynchronously.
    /// </summary>
    /// <param name="publicId">The public unique identifier of the location to retrieve.</param>
    /// <returns>An asynchronous task that represents the operation and returns the matching location base or null if not found.</returns>
    public async Task<LocationBase?> GetBaseByPublicIdAsync(Guid publicId)
        => await this.dbContext.LocationsBases.FirstOrDefaultAsync(l => l.PublicId == publicId);

    public async Task<IEnumerable<LocationSearchResult>> GetForSearchIndexingAsync()
        => await (
            from locationTranslation in this.dbContext.LocationsTranslations
            join locationBase in this.dbContext.LocationsBases
            on locationTranslation.LocationId equals locationBase.Id
            select new LocationSearchResult(
                locationBase.PublicId.ToString(),
                locationTranslation.LanguageId,
                locationTranslation.Name,
                locationTranslation.City,
                locationTranslation.Country))
            .AsNoTracking()
            .ToListAsync();

    /// <summary>
    /// Retrieves a collection of drop-off locations associated with a given pickup location.
    /// </summary>
    /// <param name="pickupLocationId">The pickup location unique identifier for which drop-off locations are sought.</param>
    /// <param name="languageId">The language identifier for which drop-off locations are sought.</param>
    /// <returns>An asynchronous task that represents the operation and returns a collection of drop-off locations.</returns>
    public async Task<IEnumerable<Location>> GetDropoffLocationsAsync(int pickupLocationId, int languageId)
    {
        var allowedLocations = await (
            from allowedLocation in this.dbContext.AllowedLocations.AsNoTracking()
            join locationBase in this.dbContext.LocationsBases.AsNoTracking()
            on allowedLocation.DropOffLocationId equals locationBase.Id
            join locationTranslation in this.dbContext.LocationsTranslations.AsNoTracking()
            on locationBase.Id equals locationTranslation.LocationId
            where allowedLocation.PickupLocationId == pickupLocationId && locationTranslation.LanguageId == languageId
            select new { locationBase, translations = locationTranslation })
                .OrderBy(l => l.translations.Country)
                .ThenBy(l => l.translations.City)
                .ThenBy(l => l.translations.Name)
                .ToListAsync();

        return allowedLocations.Select(l => new Location(l.locationBase, l.translations));
    }
}