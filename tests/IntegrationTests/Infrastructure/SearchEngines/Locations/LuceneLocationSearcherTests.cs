using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Core.DTOs;

using Infrastructure.SearchEngines.Locations;

using Lucene.Net.Index;
using Lucene.Net.Store;

using Directory = Lucene.Net.Store.Directory;

namespace IntegrationTests.Infrastructure.SearchEngines.Locations;

public sealed class LuceneLocationSearcherTests : IDisposable
{
    private readonly LuceneLocationSearcher searcher;
    private readonly string indexesDirectoryPath;

    public LuceneLocationSearcherTests()
    {
        this.searcher = new LuceneLocationSearcher();

        var contentRootPath = GetProjectDirectory();
        this.indexesDirectoryPath = Path.Combine(
            contentRootPath, "Infrastructure", "SearchEngines", "Locations", "IndexesGeneratedByTests");
    }

    [Fact]
    public void SetUpIndexes_Valid_CreatesIndex()
    {
        // Arrange
        var indexPath = Path.Combine(this.indexesDirectoryPath, "LocationIndex");

        // Act
        this.searcher.SetUpIndexes(this.indexesDirectoryPath);

        // Assert
        Assert.NotNull(this.searcher.Directory);
        Assert.NotNull(this.searcher.Analyzer);
        Assert.True(System.IO.Directory.Exists(indexPath));
    }

    [Fact]
    public void AddRange_SetUpIndexesCalled_AddsLocationsToIndex()
    {
        // Arrange
        var locations = GetFakeLocationsData();
        var indexPath = Path.Combine(this.indexesDirectoryPath, "LocationIndex");
        this.searcher.SetUpIndexes(this.indexesDirectoryPath);

        // Act
        this.searcher.AddRange(locations);

        // Assert
        using var dir = FSDirectory.Open(new DirectoryInfo(indexPath));
        using var dirReader = DirectoryReader.Open(dir);
        Assert.Equal(locations.Length, dirReader.NumDocs);
    }

    [Fact]
    public void AddRange_SetUpIndexesNotCalled_ThrowsException()
    {
        // Arrange
        var locations = GetFakeLocationsData();

        // Assert
        Assert.Throws<InvalidOperationException>(() => this.searcher.AddRange(locations));
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void Search_SpaceOrEmptySearchTerm_ReturnsEmptyIEnumerable(string searchTerm)
    {
        // Arrange
        this.searcher.SetUpIndexes(this.indexesDirectoryPath);
        this.searcher.AddRange(GetFakeLocationsData());

        // Act
        var result = this.searcher.Search(searchTerm, 1, 10);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Search_CaseInsensitive_ReturnsSameResults()
    {
        // Arrange
        var searchTerm = "Cairo";
        this.searcher.SetUpIndexes(this.indexesDirectoryPath);
        this.searcher.AddRange(GetFakeLocationsData());

        // Act
        var originalFirstResult = this.searcher.Search(searchTerm, 1, 10).ToArray()[0];
        var upperCaseFirstResult = this.searcher.Search(searchTerm.ToUpperInvariant(), 1, 10).ToArray()[0];
        var lowerCaseFirstResult = this.searcher.Search(searchTerm.ToLowerInvariant(), 1, 10).ToArray()[0];

        // Assert
        Assert.Equal(originalFirstResult.Id, upperCaseFirstResult.Id);
        Assert.Equal(originalFirstResult.Id, lowerCaseFirstResult.Id);
    }

    [Fact]
    public void Search_PrefixQuery_ReturnsResults()
    {
        // Arrange
        var searchTerm = "Cai";
        this.searcher.SetUpIndexes(this.indexesDirectoryPath);
        this.searcher.AddRange(GetFakeLocationsData());

        // Act
        var results = this.searcher.Search(searchTerm, 1, 10);

        // Assert
        Assert.StartsWith(searchTerm, results.ToArray()[0].Name);
    }

    [Fact]
    public void Search_FuzzyQueryWithDistance1_ReturnsResults()
    {
        // Arrange
        var searchTerm = "CairY";
        var fakeLocations = GetFakeLocationsData();
        this.searcher.SetUpIndexes(this.indexesDirectoryPath);
        this.searcher.AddRange(fakeLocations);

        // Act
        var results = this.searcher.Search(searchTerm, 1, 10);

        // Assert
        Assert.Contains(fakeLocations[0].Name, results.ToArray()[0].Name);
    }

    // [Fact]
    // public void Search_WhenValidSearchTermAndLanguageIdProvided_ReturnsResults()
    // {
    //     // Arrange
    //     this.searcher.SetUpIndexes(this.indexesDirectoryPath);
    //     this.searcher.AddRange(GetFakeLocationsData());

    //     // Act
    //     var results = this.searcher.Search("Location", 1, 10);

    //     // Assert
    //     Assert.AreEqual(2, results.Count);
    // }

    [Fact]
    public void Search_SetUpIndexesNotCalled_ThrowsException()
    {
        // Assert
        Assert.Throws<InvalidOperationException>(() => this.searcher.Search(string.Empty, 1, 10));
    }

    public void Dispose()
    {
        this.searcher.Dispose();
    }

    private static string GetProjectDirectory()
    => System.IO.Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName
        ?? throw new InvalidOperationException("The project directory could not be found.");

    private static LocationSearchResult[] GetFakeLocationsData()
        => new LocationSearchResult[]
            {
                new LocationSearchResult("GUID", 1, "Cairo International Airport", "Cairo", "Egypt")
            };
}