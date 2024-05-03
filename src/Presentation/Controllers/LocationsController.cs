using Core.DTOs;
using Core.Interfaces.Repositories;
using Core.Interfaces.Searchers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

/// <summary>
/// A controller class that handles requests related to locations.
/// </summary>
public class LocationsController : BaseController
{
    private readonly ILocationRepository locationRepository;
    private readonly ILocationSearcher locationSearcher;
    private readonly ILanguageRepository languageRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocationsController"/> class.
    /// </summary>
    /// <param name="locationRepository">The location repository.</param>
    /// <param name="languageRepository">The language repository.</param>
    /// <param name="locationSearcher">The location searching service.</param>
    public LocationsController(ILocationRepository locationRepository, ILanguageRepository languageRepository, ILocationSearcher locationSearcher)
    {
        this.locationRepository = locationRepository;
        this.locationSearcher = locationSearcher;
        this.languageRepository = languageRepository;
    }

    // TODO: Add integration tests for this endpoint.

    /// <summary>
    /// Searches for locations based on the specified query and results number.
    /// </summary>
    /// <param name="query">The query string to search for.</param>
    /// <param name="languageId">The language identifier of the search term.</param>
    /// <param name="resultsNumber">The maximum number of results to return.</param>
    /// <returns>Returns a collection of location DTOs if successful; otherwise, returns a bad request or not found response.</returns>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<LocationSearchResult>> Search(
        [FromQuery] string query, [FromQuery] int languageId, [FromQuery] int resultsNumber)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return this.BadRequest("query cannot be empty or white space.");
        }

        if (resultsNumber <= 0)
        {
            return this.BadRequest("resultsNumber should be greater than 0.");
        }

        var locations = this.locationSearcher.Search(query, languageId, resultsNumber);
        return this.Ok(locations);
    }

    /// <summary>
    /// Gets drop-off locations based on the specified pickup location name.
    /// </summary>
    /// <param name="pickupLocationId">The public unique identifier of the pickup location.</param>
    /// <param name="languageId">The language identifier of the search term.</param>
    /// <returns>Returns a collection of drop-off location DTOs if successful; otherwise, returns not found response.</returns>
    [HttpGet("dropoff-locations/{pickupLocationId:guid}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LocationSearchResult>>> GetDropoffLocations(
        Guid pickupLocationId, [FromQuery] int languageId)
    {
        var languages = await this.languageRepository.GetAllAsync();
        var language = languages.FirstOrDefault(l => l.Id == languageId);

        if (language is null)
        {
            return this.NotFound("Language not found.");
        }

        var location = await this.locationRepository.GetBaseByPublicIdAsync(pickupLocationId);

        if (location is null)
        {
            return this.NotFound();
        }

        var dropoffLocations = await this.locationRepository.GetDropoffLocationsAsync(location.Id, languageId);
        var dropLocationDtos = dropoffLocations.Select(l => l.ToLocationSearchResult());
        return this.Ok(dropLocationDtos);
    }
}