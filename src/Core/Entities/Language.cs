namespace Core.Entities;

/// <summary>
/// Represents a Language entity.
/// </summary>
public class Language
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Language"/> class.
    /// </summary>
    /// <param name="code">The ISO code of the language i.e. 'en-US'.</param>
    /// <param name="englishName">The name of the language in English.</param>
    /// <param name="nativeName">The name of the language in the native language.</param>
    /// <param name="flagImagePath">The path to the country flag image.</param>
    /// <param name="isRtl">A value indicating whether the language is right-to-left.</param>
    public Language(string code, string englishName, string nativeName, string flagImagePath, bool isRtl)
    {
        this.Code = code;
        this.EnglishName = englishName;
        this.NativeName = nativeName;
        this.FlagImagePath = flagImagePath;
        this.IsRtl = isRtl;
    }

    /// <summary>
    /// Gets the unique identifier of the language.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets the ISO code of the language i.e. 'en-US'.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    /// Gets the name of the language in English.
    /// </summary>
    public string EnglishName { get; private set; }

    /// <summary>
    /// Gets the name of the language in the native language.
    /// </summary>
    public string NativeName { get; private set; }

    /// <summary>
    /// Gets the path to the country flag image of the language.
    /// </summary>
    public string FlagImagePath { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the language is right-to-left.
    /// </summary>
    public bool IsRtl { get; private set; }

    /// <summary>
    /// Gets the collection of translations for a location.
    /// </summary>
    public ICollection<LocationTranslation> LocationsTranslations { get; } = new List<LocationTranslation>();

    /// <summary>
    /// Gets the collection of translations for a currency.
    /// </summary>
    public ICollection<CurrencyTranslation> CurrenciesTranslations { get; } = new List<CurrencyTranslation>();
}