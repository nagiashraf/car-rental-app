using Core.DTOs;

using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Data.Repositories;

public class CachedCurrencyRepository : ICurrencyRepository
{
    private readonly IMemoryCache memoryCache;
    private readonly CurrencyRepository decorated;

    public CachedCurrencyRepository(IMemoryCache memoryCache, CurrencyRepository decorated)
    {
        this.memoryCache = memoryCache;
        this.decorated = decorated;
    }

    // TODO: Set size limit for cache?
    public async Task<IEnumerable<Currency>> GetAllByLanguageAsync(int languageId)
    {
        string key = $"CurrenciesKey-{languageId}";

        return await this.memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                return this.decorated.GetAllByLanguageAsync(languageId);
            });
    }
}