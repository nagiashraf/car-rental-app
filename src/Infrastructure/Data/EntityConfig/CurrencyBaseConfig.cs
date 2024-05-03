namespace Infrastructure.Data.EntityConfig;

/// <summary>
/// Represents the configuration for the <see cref="CurrencyBase"/> entity.
/// </summary>
public class CurrencyBaseConfig : IEntityTypeConfiguration<CurrencyBase>
{
    /// <summary>
    /// Configures the <see cref="CurrencyBase"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<CurrencyBase> builder)
    {
        // Configure the data types and the constraints
        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(10);
    }
}