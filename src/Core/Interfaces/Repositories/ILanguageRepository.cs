using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ILanguageRepository
{
    /// <summary>
    /// Retrieves all Language entities asynchronously.
    /// </summary>
    /// <returns>A read-only list of all retrieved Language entities.</returns>
    Task<IEnumerable<Language>> GetAllAsync();
}