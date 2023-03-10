using Meetings.Integrator.Application.Abstractions;
using Meetings.Integrator.Application.Services;
using Meetings.Integrator.Core.Abstractions;
using Meetings.Integrator.Infrastructure.Extensions;
using Meetings.Integrator.Infrastructure.Persistence;
using Meetings.Integrator.Infrastructure.Persistence.Settings;
using Meetings.Integrator.Infrastructure.Services.Google.Settings;
using Meetings.Integrator.Infrastructure.Services.Microsoft;
using Meetings.Integrator.Infrastructure.Services.Microsoft.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Meetings.Integrator.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructureConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSettings<GoogleCalendarApiSettings>(section: nameof(GoogleCalendarApiSettings))
            .AddSettings<MicrosoftGraphApiSettings>(section: nameof(MicrosoftGraphApiSettings))
            .AddSettings<MongoDbSettings>(section: nameof(MongoDbSettings));

        services
            .AddScoped<IRepository, MongoRepository>()
            .AddScoped<IQueryRepository, MongoQueryRepository>()
            .AddScoped<IMicrosoftGraphApi, MicrosoftGraphApi>()
            .AddScoped<IGoogleCalendarApi, GoogleCalendarApi>();

        services.AddMongo(configuration);

        return services;
    }

    private static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoOptions = configuration.GetOptions<MongoDbSettings>(nameof(MongoDbSettings));

        return services
            .AddSingleton<IMongoClient>(serviceProvider =>
            {
                return new MongoClient(mongoOptions.ConnectionString);
            })
            .AddScoped(serviceProvider =>
            {
                var client = serviceProvider.GetRequiredService<IMongoClient>();
                return client.GetDatabase(mongoOptions.Database);
            });
    }
}
