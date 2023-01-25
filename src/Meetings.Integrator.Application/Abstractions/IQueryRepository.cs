using Meetings.Integrator.Core.Entities;

namespace Meetings.Integrator.Application.Abstractions;

public interface IQueryRepository
{
    Task<IEnumerable<Meeting>> GetByDateAsync(DateTime from, DateTime to, CancellationToken cancellationToken);
}
