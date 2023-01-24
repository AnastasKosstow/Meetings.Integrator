using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Meetings.Integrator.Infrastructure.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddSettings<TSettings>(this IServiceCollection services, string section)
        where TSettings : class
    {
        services.AddOptions<TSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(section).Bind(settings);
            });

        return services;
    }
}