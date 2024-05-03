using Core.Entities;

namespace Core.DTOs;

/// <summary>
/// Represents a full currency by combining the CurrencyBase and CurrencyTranslation entities.
/// </summary>
public record Currency
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Currency"/> class.
    /// </summary>
    /// <param name="currencyBase">The base of the currency.</param>
    /// <param name="translation">The translation of the currency.</param>
    public Currency(CurrencyBase currencyBase, CurrencyTranslation translation)
    {
        this.Id = currencyBase.Id;
        this.Code = currencyBase.Code;
        this.LanguageId = translation.LanguageId;
        this.Name = translation.Name;
    }

    /// <summary>
    /// Gets the unique identifier of the currency.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets the ISO code of the currency i.e. 'USD'.
    /// </summary>
    public string Code { get; init; }

    /// <summary>
    /// Gets the language identifier for the translation.
    /// </summary>
    public int LanguageId { get; init; }

    /// <summary>
    /// Gets the name of the currency.
    /// </summary>
    public string Name { get; init; }
}