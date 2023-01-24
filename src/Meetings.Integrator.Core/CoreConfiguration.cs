using Meetings.Integrator.Core.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Meetings.Integrator.Core;

public static class CoreConfiguration
{
    public static IServiceCollection AddCoreConfigurations(this IServiceCollection services)
    {
        services.AddSingleton<IMeetingFactory, MeetingFactory>();
        return services;
    }
}
