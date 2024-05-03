#pragma warning disable SA1313

namespace Core.DTOs;

/// <summary>
/// Represents a search result for a location.
/// </summary>
/// <param name="Id">The unique identifier of the location.</param>
/// <param name="LanguageId">The unique identifier of the language.</param>
/// <param name="Name">The name of the location.</param>
/// <param name="City">The city where the location is situated.</param>
/// <param name="Country">The country where the location is located.</param>
public record LocationSearchResult(string Id, int LanguageId, string Name, string City, string Country);