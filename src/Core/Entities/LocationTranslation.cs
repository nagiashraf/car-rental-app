namespace Core.Entities;

/// <summary>
/// Represents a translation for the Location entity and contains the language-related properties.
/// </summary>
public class LocationTranslation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocationTranslation"/> class.
    /// </summary>
    /// <param name="locationId">The unique identifier for the location.</param>
    /// <param name="languageId">The unique identifier for the language.</param>
    /// <param name="name">The name of the location.</param>
    /// <param name="city">The city where the location is situated.</param>
    /// <param name="country">The country where the location is located.</param>
    public LocationTranslation(int locationId, int languageId, string name, string city, string country)
    {
        this.LocationId = locationId;
        this.LanguageId = languageId;
        this.Name = name;
        this.City = city;
        this.Country = country;
    }

    /// <summary>
    /// Gets the unique identifier for the location.
    /// </summary>
    public int LocationId { get; private set; }

    /// <summary>
    /// Gets the language identifier for the translation.
    /// </summary>
    public int LanguageId { get; private set; }

    /// <summary>
    /// Gets the name of the location.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the city where the location is situated.
    /// </summary>
    public string City { get; private set; }

    /// <summary>
    /// Gets the country where the location is located.
    /// </summary>
    public string Country { get; private set; }

    /// <summary>
    /// Gets the location base.
    /// </summary>
    public LocationBase LocationBase { get; private set; } = null!;

    /// <summary>
    /// Gets the language in which the location is translated.
    /// </summary>
    public Language Language { get; private set; } = null!;
}
