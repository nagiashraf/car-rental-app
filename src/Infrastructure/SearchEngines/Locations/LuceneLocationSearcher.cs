using Core.DTOs;
using Core.Interfaces.Searchers;

using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Search.Payloads;
using Lucene.Net.Store;
using Lucene.Net.Util;

using Directory = Lucene.Net.Store.Directory;

namespace Infrastructure.SearchEngines.Locations;

/// <summary>
/// Provides functionality for searching locations using Lucene.NET indexing. This class MUST be injected as a singleton.
/// </summary>
public sealed class LuceneLocationSearcher : ILocationSearcher, IDisposable
{
    private const LuceneVersion LuceneNetVersion = LuceneVersion.LUCENE_48;
    private IndexWriter? indexWriter;

    /// <summary>
    /// Enumeration representing different fields in the location index.
    /// </summary>
    public enum IndexField
    {
        /// <summary>
        /// Represents the location ID field.
        /// </summary>
        LocationId,

        /// <summary>
        /// Represents the language ID field.
        /// </summary>
        LanguageId,

        /// <summary>
        /// Represents the name field.
        /// </summary>
        Name,

        /// <summary>
        /// Represents the city field.
        /// </summary>
        City,

        /// <summary>
        /// Represents the country field.
        /// </summary>
        Country
    }

    /// <summary>
    /// Gets the directory where the Lucene index is stored.
    /// </summary>
    public Directory? Directory { get; private set; }

    /// <summary>
    /// Gets the analyzer used for indexing and searching.
    /// </summary>
    public Analyzer? Analyzer { get; private set; }

   /// <summary>
    /// Sets up the Lucene indexes for location searching. This method must be called before using the searcher.
    /// </summary>
    /// <param name="indexesDirectoryPath">The directory path where the indexes will be stored.</param>
    public void SetUpIndexes(string indexesDirectoryPath)
    {
        string indexPath = Path.Combine(indexesDirectoryPath, "LocationIndex");
        this.Directory = FSDirectory.Open(indexPath);
        this.Analyzer = new LowerCaseAnalyzer();
        var indexConfig = new IndexWriterConfig(LuceneNetVersion, this.Analyzer)
        {
            OpenMode = OpenMode.CREATE
        };
        this.indexWriter = new IndexWriter(this.Directory, indexConfig);
    }

    /// <summary>
    /// Adds a range of locations to the Lucene index.
    /// </summary>
    /// <param name="locations">The locations to add to the index.</param>
    public void AddRange(IEnumerable<LocationSearchResult> locations)
    {
        if (this.indexWriter is null)
        {
            throw new InvalidOperationException("Index writer is not initialized. Call SetUpIndexes first.");
        }

        var locationIdField = new StringField(
            IndexField.LocationId.ToString(),
            string.Empty,
            Field.Store.YES);

        var languageIdField = new Int32Field(
            IndexField.LanguageId.ToString(),
            0,
            Field.Store.YES);

        var nameField = new TextField(
            IndexField.Name.ToString(),
            string.Empty,
            Field.Store.YES);

        var cityField = new TextField(
            IndexField.City.ToString(),
            string.Empty,
            Field.Store.YES);

        var countryField = new TextField(
            IndexField.Country.ToString(),
            string.Empty,
            Field.Store.YES);

        var doc = new Document
        {
            locationIdField,
            languageIdField,
            nameField,
            cityField,
            countryField
        };

        foreach (LocationSearchResult location in locations)
        {
            locationIdField.SetStringValue(location.Id);
            languageIdField.SetInt32Value(location.LanguageId);
            nameField.SetStringValue(location.Name);
            cityField.SetStringValue(location.City);
            countryField.SetStringValue(location.Country);
            this.indexWriter.AddDocument(doc);
        }

        this.indexWriter.Commit();
    }

    /// <summary>
    /// Searches for locations based on the provided search term, language ID, and maximum number of results.
    /// </summary>
    /// <param name="searchTerm">The term to search for.</param>
    /// <param name="languageId">The unique identifier of the language to filter the search.</param>
    /// <param name="maxResultsNumber">The maximum number of results to return.</param>
    /// <returns>A collection of location search results.</returns>
    public IEnumerable<LocationSearchResult> Search(string searchTerm, int languageId, int maxResultsNumber)
    {
        if (this.Directory is null)
        {
            throw new InvalidOperationException("Index directory is not initialized. Call SetUpIndexes first.");
        }

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Enumerable.Empty<LocationSearchResult>();
        }

        searchTerm = searchTerm.Trim().ToLowerInvariant();

        using var dirReader = DirectoryReader.Open(this.Directory);
        var searcher = new IndexSearcher(dirReader);

        var locationQuery = new BooleanQuery();
        AddFieldQuery(locationQuery, searchTerm, IndexField.Name, 4.0f);
        AddFieldQuery(locationQuery, searchTerm, IndexField.City, 10.0f);
        AddFieldQuery(locationQuery, searchTerm, IndexField.Country, 8.0f);

        var languageBoostQuery = new LanguageBoostQuery(locationQuery, languageId);

        var topDocs = searcher.Search(languageBoostQuery, maxResultsNumber).ScoreDocs;

        var results = topDocs.Select(scoreDoc =>
        {
            Document doc = searcher.Doc(scoreDoc.Doc);
            return new LocationSearchResult(
                doc.Get(IndexField.LocationId.ToString()),
                int.Parse(doc.Get(IndexField.LanguageId.ToString())),
                doc.Get(IndexField.Name.ToString()),
                doc.Get(IndexField.City.ToString()),
                doc.Get(IndexField.Country.ToString()));
        }).ToList();

        return results;
    }

    /// <summary>
    /// Disposes resources used by the LocationSearcher.
    /// </summary>
    public void Dispose()
    {
        this.indexWriter?.Dispose();
        this.Analyzer?.Dispose();
        this.Directory?.Dispose();
    }

    private static void AddFieldQuery(BooleanQuery query, string searchTerm, IndexField field, float boost)
    {
        string fieldName = field.ToString();
        int fuzzyQueryDistance = 1;

        query.Add(
            new PrefixQuery(
                new Term(fieldName, searchTerm))
                {
                    Boost = boost
                },
            Occur.SHOULD);

        query.Add(
            new FuzzyQuery(
                new Term(fieldName, searchTerm),
                fuzzyQueryDistance),
            Occur.SHOULD);

        query.Add(
            new PayloadTermQuery(
                new Term(fieldName, searchTerm),
                new AveragePayloadFunction()),
            Occur.SHOULD);
    }
}