namespace Core.Entities;

/// <summary>
/// Represents a base class for the currency entity that contains the language-neutral properties.
/// </summary>
public class CurrencyBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CurrencyBase"/> class.
    /// </summary>
    /// <param name="code">The ISO code of the currency.</param>
    public CurrencyBase(string code)
    {
        this.Code = code;
    }

    /// <summary>
    /// Gets the unique identifier of the currency.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets the ISO code of the currency i.e. 'USD'.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    /// Gets the collection of translations of this currency.
    /// </summary>
    public ICollection<CurrencyTranslation> Translations { get; } = new List<CurrencyTranslation>();
}