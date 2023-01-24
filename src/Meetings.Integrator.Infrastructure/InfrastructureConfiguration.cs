using Meetings.Integrator.Application.Services;
using Meetings.Integrator.Infrastructure.Extensions;
using Meetings.Integrator.Infrastructure.Services.Microsoft;
using Meetings.Integrator.Infrastructure.Services.Microsoft.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Meetings.Integrator.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructureConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSettings<MicrosoftGraphApiSettings>(section: nameof(MicrosoftGraphApiSettings))
            .AddScoped<IMicrosoftGraphApi, MicrosoftGraphApi>();

        return services;
    }
}
