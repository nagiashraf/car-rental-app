namespace Infrastructure.Data.EntityConfig;

/// <summary>
/// Represents the configuration for the <see cref="CurrencyTranslation"/> entity.
/// </summary>
public class CurrencyTranslationConfig : IEntityTypeConfiguration<CurrencyTranslation>
{
    /// <summary>
    /// Configures the <see cref="CurrencyTranslation"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<CurrencyTranslation> builder)
    {
        builder.HasKey(ct => new { ct.CurrencyId, ct.LanguageId });

        // Configure the foreign keys
        builder.HasOne(ct => ct.CurrencyBase)
        .WithMany(c => c.Translations)
        .HasForeignKey(ct => ct.CurrencyId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ct => ct.Language)
        .WithMany(l => l.CurrenciesTranslations)
        .HasForeignKey(ct => ct.LanguageId)
        .OnDelete(DeleteBehavior.Cascade);

        // Configure the data types and the constraints
        builder.Property(ct => ct.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}