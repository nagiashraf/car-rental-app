using Core.Interfaces.Repositories;
using Core.Interfaces.Searchers;

using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Data.DataSeeders;

using Microsoft.Extensions.Options;

using Presentation;

using Serilog;

// TODO: Clean up
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(path: "logs/bootstrap-logger.txt")
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;

    builder.Services.AddControllers()
        .AddApplicationPart(presentationAssembly);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

    builder.Services.AddCors(options =>
        options.AddPolicy(
            "AllowAngularClientPolicy", policy => policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()));

    builder.Services
        .AddInfrastructure(builder.Configuration)
        .AddPresentation(builder.Configuration);

    var app = builder.Build();

    using var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;

    var context = serviceProvider.GetRequiredService<CarRentalDbContext>();
    var searchIndexesDirectoryPathSegment = serviceProvider.GetRequiredService<IOptions<EnvironmentConfig>>().Value.SearchIndexesDirectoryPath;
    var seedDataFilePathSegment = serviceProvider.GetRequiredService<IOptions<EnvironmentConfig>>().Value.SeedDataDirectoryPath;
    var searchIndexesDirectoryPath = Path.Combine(app.Environment.ContentRootPath, searchIndexesDirectoryPathSegment);
    var seedDataFilePath = Path.Combine(app.Environment.ContentRootPath, seedDataFilePathSegment);

    await new LanguageSeeder(context, GetLogger<LanguageSeeder>(), seedDataFilePath).SeedAsync();
    await new LocationBaseSeeder(context, GetLogger<LocationBaseSeeder>(), seedDataFilePath).SeedAsync();
    await new LocationTranslationSeeder(context, GetLogger<LocationTranslationSeeder>(), seedDataFilePath).SeedAsync();
    await new AllowedLocationSeeder(context, GetLogger<AllowedLocationSeeder>(), seedDataFilePath).SeedAsync();
    await new CurrencyBaseSeeder(context, GetLogger<CurrencyBaseSeeder>(), seedDataFilePath).SeedAsync();
    await new CurrencyTranslationSeeder(context, GetLogger<CurrencyTranslationSeeder>(), seedDataFilePath).SeedAsync();

    var locationRepository = serviceProvider.GetRequiredService<ILocationRepository>();
    var locationSearcher = serviceProvider.GetRequiredService<ILocationSearcher>();

    var searcherSeederLocations = await locationRepository.GetForSearchIndexingAsync();
    locationSearcher.SetUpIndexes(searchIndexesDirectoryPath);
    locationSearcher.AddRange(searcherSeederLocations);

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseCors("AllowAngularClientPolicy");

    app.UseAuthorization();

    app.MapControllers();

    await app.RunAsync();

    ILogger<T> GetLogger<T>() => serviceProvider.GetRequiredService<ILogger<T>>();
}
catch (Exception ex)
{
    // reason: https://andrewlock.net/exploring-dotnet-6-part-5-supporting-ef-core-tools-with-webapplicationbuilder/#updates-to-the-hostfactoryresolver
    // workaround: https://stackoverflow.com/questions/70247187/microsoft-extensions-hosting-hostfactoryresolverhostinglistenerstopthehostexce
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

/// <summary>
/// Making the Program class public using a partial class declaration to expose it to the integration tests project.
/// </summary>
#pragma warning disable CA1050
#pragma warning disable S1118
public partial class Program
{
}