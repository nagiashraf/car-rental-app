using Asp.Versioning;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config)
    {
        services.AddApiVersioning(options =>
            options.ApiVersionReader = new UrlSegmentApiVersionReader())
            .AddMvc().AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        

        services.Configure<EnvironmentConfig>(config.GetSection(nameof(EnvironmentConfig)));

        return services;
    }
}