using Microsoft.Extensions.DependencyInjection;
using Meetings.Integrator.Application.Abstractions;

namespace Meetings.Integrator.Application.Dispatchers;

internal sealed class InMemoryCommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider serviceProvider;

    public InMemoryCommandDispatcher(IServiceProvider serviceProvider)
        =>
        this.serviceProvider = serviceProvider;


    public async Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : class, ICommand
    {
        using var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        await handler.HandleAsync(command, cancellationToken);
    }
}