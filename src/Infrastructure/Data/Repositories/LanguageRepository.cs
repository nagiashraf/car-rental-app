
namespace Infrastructure.Data.Repositories;

public class LanguageRepository : ILanguageRepository
{
    private readonly CarRentalDbContext dbContext;

    public LanguageRepository(CarRentalDbContext dbContext) => this.dbContext = dbContext;

    /// <summary>
    /// Retrieves all Language entities from the database asynchronously.
    /// </summary>
    /// <returns>A read-only list of Languages.</returns>
    public async Task<IEnumerable<Language>> GetAllAsync()
        => await this.dbContext.Set<Language>()
            .AsNoTracking()
            .ToListAsync();
}