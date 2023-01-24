namespace Meetings.Integrator.Core.Abstractions;

public interface IFactory<out TEntity> where TEntity : IAggregateRoot
{
    TEntity Build();
}
