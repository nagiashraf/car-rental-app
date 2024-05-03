namespace Core.Entities;

/// <summary>
/// Represents an allowed location entity that is a joint between a location and itself.
/// </summary>
public class AllowedLocation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AllowedLocation"/> class.
    /// </summary>
    /// <param name="pickupLocationId">The unique identifier of the pickup location.</param>
    /// <param name="dropOffLocationId">The unique identifier of the dropoff location.</param>
    public AllowedLocation(int pickupLocationId, int dropOffLocationId)
    {
        this.PickupLocationId = pickupLocationId;
        this.DropOffLocationId = dropOffLocationId;
    }

    /// <summary>
    /// Gets the ID of the pickup location associated with the location.
    /// </summary>
    public int PickupLocationId { get; private set; }

    /// <summary>
    /// Gets the ID of the dropoff location associated with the location.
    /// </summary>
    public int DropOffLocationId { get; private set; }

    /// <summary>
    /// Gets a pickup location associated with the location.
    /// </summary>
    public LocationBase PickUpLocation { get; private set; } = null!;

    /// <summary>
    /// Gets a dropoff location associated with the location.
    /// </summary>
    public LocationBase DropOffLocation { get; private set; } = null!;
}