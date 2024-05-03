namespace Infrastructure.Data.EntityConfig;

/// <summary>
/// Represents the configuration for the <see cref="Language"/> entity.
/// </summary>
public class LanguageConfig : IEntityTypeConfiguration<Language>
{
    /// <summary>
    /// Configures the <see cref="Language"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        // Configure the data types and the constraints
        builder.Property(l => l.EnglishName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(l => l.NativeName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(l => l.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(l => l.FlagImagePath)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(l => l.IsRtl)
            .IsRequired();

        builder.HasIndex(l => l.Code)
            .IsUnique();
    }
}