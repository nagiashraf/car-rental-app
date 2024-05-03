namespace Core.Entities;

/// <summary>
/// Represents a base class for the Location entity that contains the language-neutral properties.
/// </summary>
public class LocationBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocationBase"/> class.
    /// </summary>
    /// <param name="phoneNumber">The phone number of the location.</param>
    public LocationBase(string phoneNumber)
    {
        this.PhoneNumber = phoneNumber;
    }

    /// <summary>
    /// Gets the unique identifier of the location.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets or sets a GUID identifier of the location that is exposed to the client.
    /// </summary>
    public Guid PublicId { get; set; }

    /// <summary>
    /// Gets the phone number of the location.
    /// </summary>
    public string PhoneNumber { get; private set; }

    /// <summary>
    /// Gets the collection of translations of this location.
    /// </summary>
    public ICollection<LocationTranslation> Translations { get; } = new List<LocationTranslation>();

    /// <summary>
    /// Gets the collection of allowed drop-off locations when this location is the pick-up.
    /// </summary>
    public ICollection<AllowedLocation> AllowedDropOffLocations { get; } = new List<AllowedLocation>();
}