using Core.DTOs;

namespace Core.Interfaces.Repositories;

public interface ICurrencyRepository
{
    Task<IEnumerable<Currency>> GetAllByLanguageAsync(int languageId);
}