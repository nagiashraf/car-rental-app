using Core.Entities;

namespace Core.DTOs;

/// <summary>
/// Represents a full location by combining the LocationBase and LocationTranslation entities.
/// </summary>
public record Location
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Location"/> class.
    /// </summary>
    /// <param name="locationBase">The base of the location.</param>
    /// <param name="translation">The translation of the location.</param>
    public Location(LocationBase locationBase, LocationTranslation translation)
    {
        this.Id = locationBase.Id;
        this.PublicId = locationBase.PublicId;
        this.PhoneNumber = locationBase.PhoneNumber;
        this.LanguageId = translation.LanguageId;
        this.Name = translation.Name;
        this.City = translation.City;
        this.Country = translation.Country;
    }

    /// <summary>
    /// Gets the unique identifier of the location.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets a GUID identifier of the location that is exposed to the client.
    /// </summary>
    public Guid PublicId { get; set; }

    /// <summary>
    /// Gets the phone number of the location.
    /// </summary>
    public string PhoneNumber { get; init; }

    /// <summary>
    /// Gets the language identifier for the translation.
    /// </summary>
    public int LanguageId { get; init; }

    /// <summary>
    /// Gets the name of the location.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets the city where the location is situated.
    /// </summary>
    public string City { get; init; }

    /// <summary>
    /// Gets the country where the location is located.
    /// </summary>
    public string Country { get; init; }
}