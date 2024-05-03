using Core.DTOs;
using Core.Entities;

using Presentation.DTOs;

namespace Presentation;

// TODO: Add unit tests
// TODO: Add extension methods for lists i.e. List<Location>.ToLocationSearchResult()

/// <summary>
/// A static class that provides extension methods for mapping different objects.
/// </summary>
public static class Mapper
{
    /// <summary>
    /// Maps a Location object to a LocationSearchResult object.
    /// </summary>
    /// <param name="location">The Location object to map.</param>
    /// <returns>The mapped LocationSearchResult object.</returns>
    public static LocationSearchResult ToLocationSearchResult(this Location location)
        => new (
            location.PublicId.ToString(),
            location.LanguageId,
            location.Name,
            location.City,
            location.Country);

    public static LanguageDto ToLanguageDto(this Language language, string baseUrl)
        => new (
            language.Id,
            language.Code,
            language.EnglishName,
            language.NativeName,
            baseUrl + language.FlagImagePath,
            language.IsRtl);

    public static CurrencyDto ToCurrencyDto(this Currency currency)
    {
        return new (
            currency.Id,
            currency.Code,
            currency.Name);
    }
}