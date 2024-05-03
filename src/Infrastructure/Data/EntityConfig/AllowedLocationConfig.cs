namespace Infrastructure.Data.EntityConfig;

/// <summary>
/// Represents the configuration for the <see cref="AllowedLocation"/> entity.
/// </summary>
public class AllowedLocationConfig : IEntityTypeConfiguration<AllowedLocation>
{
    /// <summary>
    /// Configures the <see cref="AllowedLocation"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<AllowedLocation> builder)
    {
        builder.HasKey(al => new { al.PickupLocationId, al.DropOffLocationId });

        // Configure the foreign keys
        builder.HasOne(al => al.PickUpLocation)
            .WithMany(l => l.AllowedDropOffLocations)
            .HasForeignKey(al => al.PickupLocationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(al => al.DropOffLocation)
            .WithMany()
            .HasForeignKey(al => al.DropOffLocationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}