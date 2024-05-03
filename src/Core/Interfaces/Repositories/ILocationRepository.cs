using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces.Repositories;

// TODO: Add cancelation token: https://github.com/Erikvdv/realworlddotnet/blob/main/src/Data/Services/ConduitRepository.cs
/// <summary>
/// Represents a repository interface for managing operations related to the 'Location' entity.
/// Inherits from the generic repository interface for common repository functionality.
/// </summary>
public interface ILocationRepository
{
    /// <summary>
    /// Asynchronously retrieves a location by its name.
    /// </summary>
    /// <param name="locationGuid">The GUID of the location to retrieve.</param>
    /// <returns>An asynchronous task that represents the operation and returns the matching location or null if not found.</returns>
    Task<LocationBase?> GetBaseByPublicIdAsync(Guid publicId);

    Task<IEnumerable<LocationSearchResult>> GetForSearchIndexingAsync();

    /// <summary>
    /// Asynchronously retrieves a collection of drop-off locations associated with a given pickup location.
    /// </summary>
    /// <param name="pickupLocationId">The pickup location unique identifier for which drop-off locations are sought.</param>
    /// <param name="languageId">The language identifier for which drop-off locations are sought.</param>
    /// <returns>An asynchronous task that represents the operation and returns a collection of drop-off locations.</returns>
    Task<IEnumerable<Location>> GetDropoffLocationsAsync(int pickupLocationId, int languageId);
}