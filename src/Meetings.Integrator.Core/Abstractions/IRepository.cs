using Meetings.Integrator.Core.Entities;

namespace Meetings.Integrator.Core.Abstractions;

public interface IRepository
{
    Task AddAsync(Meeting meeting, CancellationToken cancellationToken);
    Task<Meeting> GetAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
}
