using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Data.Repositories;

public class CachedLanguageRepository : ILanguageRepository
{
    private readonly IMemoryCache memoryCache;
    private readonly LanguageRepository decorated;

    public CachedLanguageRepository(IMemoryCache memoryCache, LanguageRepository decorated)
    {
        this.memoryCache = memoryCache;
        this.decorated = decorated;
    }

    public async Task<IEnumerable<Language>> GetAllAsync()
    {
        string key = "LanguagesKey";

        return await this.memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                return this.decorated.GetAllAsync();
            });
    }
}