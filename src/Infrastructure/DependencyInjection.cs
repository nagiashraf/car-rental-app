using Core.Interfaces.Searchers;

using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.SearchEngines.Locations;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        string? connectionString = config.GetConnectionString("CarRentalConnection");
        services.AddDbContextPool<CarRentalDbContext>(options => options.UseSqlServer(connectionString));

        services.AddMemoryCache();

        services.AddScoped<LanguageRepository>();
        services.AddScoped<ILanguageRepository, CachedLanguageRepository>();
        services.AddScoped<CurrencyRepository>();
        services.AddScoped<ICurrencyRepository, CachedCurrencyRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddSingleton<ILocationSearcher, LuceneLocationSearcher>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}