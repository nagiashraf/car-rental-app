namespace Infrastructure.Data.EntityConfig;

/// <summary>
/// Represents the configuration for the <see cref="LocationTranslation"/> entity.
/// </summary>
public class LocationTranslationsConfig : IEntityTypeConfiguration<LocationTranslation>
{
    /// <summary>
    /// Configures the <see cref="LocationTranslation"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<LocationTranslation> builder)
    {
        builder.HasKey(lt => new { lt.LocationId, lt.LanguageId });

        // Configure the foreign keys
        builder.HasOne(lt => lt.LocationBase)
        .WithMany(l => l.Translations)
        .HasForeignKey(lt => lt.LocationId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(lt => lt.Language)
        .WithMany(l => l.LocationsTranslations)
        .HasForeignKey(lt => lt.LanguageId)
        .OnDelete(DeleteBehavior.Cascade);

        // Configure the data types and the constraints
        builder.Property(lt => lt.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(lt => lt.City)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(lt => lt.Country)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(l => new { l.Country, l.City, l.Name });
    }
}