namespace Meetings.Integrator.Application.Abstractions;

public interface IQueryDispatcher
{
    Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        where TResult : class;
}