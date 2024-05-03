// using Core.Enums;
// using Core.Models;
// using Data;
// using Data.Repositories;

// namespace Tests.Data.Tests.Repositories;

// public class GenericRepositoryTests : IClassFixture<TestDatabaseFixture>
// {
//     public GenericRepositoryTests(TestDatabaseFixture fixture) => this.Fixture = fixture;

//     public TestDatabaseFixture Fixture { get; }

//     [Fact]
//     public async Task GetAllAsync_ReturnsAllEntities()
//     {
//         // Arrange
//         using var context = this.Fixture.CreateContext();
//         var repository = new GenericRepository<Vehicle>(context);

//         // Act
//         var result = await repository.GetAllAsync();

//         // Assert
//         Assert.Equal(25, result.Count);
//         Assert.Contains(result, v => v.Make == "Toyota");
//     }

//     [Fact]
//     public async Task GetByIdAsync_ReturnsEntityWithGivenId_OrNullIfNotFound()
//     {
//         // Arrange
//         using var context = this.Fixture.CreateContext();
//         var repository = new GenericRepository<Vehicle>(context);

//         // Act
//         var result1 = await repository.GetByIdAsync(1);
//         var result2 = await repository.GetByIdAsync(-1);

//         // Assert
//         Assert.NotNull(result1);
//         Assert.Equal("Ford", result1!.Make);
//         Assert.Null(result2);
//     }

//     [Fact]
//     public async Task Create_AddsEntityToDbContext()
//     {
//         // Arrange
//         using var context = this.Fixture.CreateContext();
//         await context.Database.BeginTransactionAsync();
//         var repository = new GenericRepository<Vehicle>(context);
//         var maxId = context.Vehicles.Max(v => v.Id);

//         // Act
//         var vehicle = new Vehicle(
//             make: "Tesla",
//             model: "TM",
//             imageUrl: "https://example.com/tesla.jpg")
//         {
//             Year = 2025,
//             Transmission = Transmission.Automatic,
//             NumberOfSeats = 5,
//             PricePerDay = 100,
//             AssignedLocationId = 1
//         };
//         repository.Create(vehicle);
//         await context.SaveChangesAsync();

//         // Assert
//         Assert.Equal(maxId + 1, vehicle.Id);
//         context.ChangeTracker.Clear();
//         var createdVehicle = await context.Set<Vehicle>().FindAsync(vehicle.Id);
//         Assert.NotNull(createdVehicle);
//         Assert.Equal("Tesla", createdVehicle!.Make);
//         Assert.Equal(2025, createdVehicle.Year);
//     }

//     [Fact]
//     public async Task Update_UpdatesEntityInDbContext()
//     {
//         // Arrange
//         using var context = this.Fixture.CreateContext();
//         await context.Database.BeginTransactionAsync();
//         var repository = new GenericRepository<Vehicle>(context);

//         // Act
//         var vehicle = await context.Set<Vehicle>().FindAsync(2);
//         vehicle!.Make = "Tttt";
//         vehicle.Year = 1111;
//         repository.Update(vehicle);
//         await context.SaveChangesAsync();

//         // Assert
//         context.ChangeTracker.Clear();
//         var updatedCar = await context.Set<Vehicle>().FindAsync(vehicle.Id);
//         Assert.NotNull(updatedCar);
//         Assert.Equal("Tttt", updatedCar!.Make);
//         Assert.Equal(1111, updatedCar.Year);
//     }

//     [Fact]
//     public async Task Delete_DeletesEntityFromDbContext()
//     {
//         // Arrange
//         using var context = this.Fixture.CreateContext();
//         await context.Database.BeginTransactionAsync();
//         var repository = new GenericRepository<Vehicle>(context);

//         // Act
//         var vehicle = await context.Set<Vehicle>().FindAsync(3);
//         repository.Delete(vehicle!);
//         await context.SaveChangesAsync();

//         // Assert
//         var deletedVehicle = await context.Set<Vehicle>().FindAsync(vehicle!.Id);
//         Assert.Null(deletedVehicle);
//     }
// }