using Core.DTOs;

namespace Core.Interfaces.Searchers;

public interface ILocationSearcher
{
    void SetUpIndexes(string indexesDirectoryPath);

    void AddRange(IEnumerable<LocationSearchResult> locations);

    IEnumerable<LocationSearchResult> Search(string searchTerm, int languageId, int maxResultsNumber);
}