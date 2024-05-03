namespace Core.Entities;

/// <summary>
/// Represents a translation for the currency entity and contains the language-related properties.
/// </summary>
public class CurrencyTranslation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CurrencyTranslation"/> class.
    /// </summary>
    /// <param name="name">The name of the currency.</param>
    /// <param name="currencyId">The unique identifier for the currency.</param>
    /// <param name="languageId">The language identifier for the language.</param>
    public CurrencyTranslation(string name, int currencyId, int languageId)
    {
        this.Name = name;
        this.CurrencyId = currencyId;
        this.LanguageId = languageId;
    }

    /// <summary>
    /// Gets the unique identifier for the currency.
    /// </summary>
    public int CurrencyId { get; private set; }

    /// <summary>
    /// Gets the language identifier for the translation.
    /// </summary>
    public int LanguageId { get; private set; }

    /// <summary>
    /// Gets the name of the currency.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the currency base.
    /// </summary>
    public CurrencyBase CurrencyBase { get; private set; } = null!;

    /// <summary>
    /// Gets the language in which the currency is translated.
    /// </summary>
    public Language Language { get; private set; } = null!;
}