using Meetings.Integrator.Application.Abstractions;
using Meetings.Integrator.Application.Dispatchers;
using Microsoft.Extensions.DependencyInjection;
using North.Availability.Application.Dispatchers;
using System.Reflection;

namespace Meetings.Integrator.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationConfigurations(this IServiceCollection services)
    {
        services
            .AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>()
            .AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();

        GetHandlersClassesByType(typeof(ICommandHandler<>)).
            ToList()
            .ForEach(commandHandler =>
            {
                // ICommandHandler`1 - search for ICommandHandler interface with one generic argument
                services.AddScoped(commandHandler.GetInterface("ICommandHandler`1"), commandHandler);
            });

        GetHandlersClassesByType(typeof(IQueryHandler<,>))
            .ToList()
            .ForEach(queryHandler =>
            {
                // IQueryHandler`2 - search for IQueryHandler interface with two generic arguments
                services.AddScoped(queryHandler.GetInterface("IQueryHandler`2"), queryHandler);
            });

        return services;
    }

    private static IEnumerable<Type> GetHandlersClassesByType(Type implementationType)
    {
        return Assembly.GetExecutingAssembly()
            .ExportedTypes
            .Where(type =>
            {
                var implementType = type
                    .GetInterfaces()
                    .Any(@interface => @interface.IsGenericType &&
                                       @interface.GetGenericTypeDefinition() == implementationType);

                return implementType;
            });
    }
}
