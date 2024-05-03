// using Core.Models;

// using Data.Repositories;

// namespace Tests.Data.Tests.Repositories;

// public class LocationRepositoryTests : IClassFixture<TestDatabaseFixture>
// {
//     public LocationRepositoryTests(TestDatabaseFixture fixture) => this.Fixture = fixture;

//     public TestDatabaseFixture Fixture { get; }

//     // [Theory]
//     // [InlineData("cairo", 3)]
//     // [InlineData("paris", 2)]
//     // [InlineData(" ", 0)]
//     // [InlineData("new york", 1)]
//     // [Fact]
//     // public async Task SearchAsync_WithValidSearchTerm_ReturnsMatchingLocations()
//     // {
//     //     // Arrange
//     //     using var context = this.Fixture.CreateContext();
//     //     var repository = new LocationRepository(context);

//     //     // Act
//     //     var searchTerm = "c";
//     //     var maxResultsNumber = 8;
//     //     var result = await repository.SearchAsync(searchTerm, maxResultsNumber);

//     //     // Assert
//     //     Assert.NotNull(result);
//     //     Assert.Equal(maxResultsNumber, result.Count());
//     //     Assert.StartsWith("c", result.First().Name, StringComparison.OrdinalIgnoreCase);
//     //     Assert.StartsWith("c", result.ElementAt(4).City, StringComparison.OrdinalIgnoreCase);
//     //     Assert.StartsWith("c", result.Last().Country, StringComparison.OrdinalIgnoreCase);
//     // }

//     [Fact]
//     public async Task GetByNameAsync_WhenNameExists_ReturnsLocation()
//     {
//         // Arrange
//         using var context = this.Fixture.CreateContext();
//         var repository = new LocationRepository(context);

//         // Act
//         var result = await repository.GetByNameAsync("Cairo International Airport");

//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal("Cairo International Airport", result!.Name);
//     }

//     [Fact]
//     public async Task GetByNameAsync_WhenNameDoesNotExist_ReturnsNull()
//     {
//         // Arrange
//         using var context = this.Fixture.CreateContext();
//         var repository = new LocationRepository(context);

//         // Act
//         var result = await repository.GetByNameAsync("Wrong Name");

//         // Assert
//         Assert.Null(result);
//     }

//     [Fact]
//     public async Task GetDropoffLocations_WhenValidPickupLocationIsGiven_ReturnCorrectLocations()
//     {
//         // Arrange
//         using var context = this.Fixture.CreateContext();
//         var repository = new LocationRepository(context);
//         var pickupLocation = new Location("name", "city", "country", "phone no.") { Id = 1 };

//         // Act
//         var result = await repository.GetDropoffLocations(pickupLocation);

//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(7, result.Count());
//         Assert.Contains(result, l => l.Id == 2 && l.Name == "San Francisco International Airport");
//         Assert.Contains(result, l => l.Id == 3 && l.Name == "McCarran International Airport");
//     }

//     [Fact]
//     public async Task GetDropoffLocations_WhenInvalidPickupLocationIsGiven_ReturnEmptyCollection()
//     {
//         // Arrange
//         using var context = this.Fixture.CreateContext();
//         var repository = new LocationRepository(context);
//         var pickupLocation = new Location("name", "city", "country", "phone no.") { Id = -1 };

//         // Act
//         var result = await repository.GetDropoffLocations(pickupLocation);

//         // Assert
//         Assert.NotNull(result);
//         Assert.Empty(result);
//     }
// }