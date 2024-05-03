using Core.DTOs;

namespace Infrastructure.Data.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly CarRentalDbContext dbContext;

    public CurrencyRepository(CarRentalDbContext dbContext) => this.dbContext = dbContext;

    // TODO: compare with the implementation of the GetDropoffLocations method in the LocationRepository
    public async Task<IEnumerable<Currency>> GetAllByLanguageAsync(int languageId)
        => await (
            from currencyBase in this.dbContext.CurrenciesBases.AsNoTracking()
            join currencyTranslation in this.dbContext.CurrenciesTranslations.AsNoTracking()
            on currencyBase.Id equals currencyTranslation.CurrencyId
            where currencyTranslation.LanguageId == languageId
            select new Currency(currencyBase, currencyTranslation))
        .ToListAsync();
}