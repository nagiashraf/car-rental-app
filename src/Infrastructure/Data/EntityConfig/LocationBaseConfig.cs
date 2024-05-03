namespace Infrastructure.Data.EntityConfig;

/// <summary>
/// Represents the configuration for the <see cref="LocationBase"/> entity.
/// </summary>
public class LocationBaseConfig : IEntityTypeConfiguration<LocationBase>
{
    /// <summary>
    /// Configures the <see cref="LocationBase"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<LocationBase> builder)
    {
        // Configure the data types and the constraints
        builder.Property(l => l.PublicId)
            .IsRequired()
            .HasDefaultValueSql("NEWID()");

        builder.Property(l => l.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(l => l.PublicId)
            .IsUnique();
    }
}