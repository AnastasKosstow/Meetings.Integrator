using Meetings.Integrator.Core.Abstractions;
using Meetings.Integrator.Core.Entities;
using Meetings.Integrator.Core.Factories;
using Meetings.Integrator.Infrastructure.Extensions;
using Meetings.Integrator.Infrastructure.Persistence.Documents;
using MongoDB.Driver;

namespace Meetings.Integrator.Infrastructure.Persistence;

internal sealed class MongoRepository : IRepository
{
    private readonly IMeetingFactory factory;
    private readonly IMongoCollection<MeetingDocument> Collection;

    public MongoRepository(IMeetingFactory factory, IMongoDatabase database)
    {
        this.factory = factory;
        Collection = database.GetCollection<MeetingDocument>("meetings");
    }

    public Task AddAsync(Meeting meeting, CancellationToken cancellationToken)
    {
        var entity = meeting.AsDocument();
        return Collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task<Meeting> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var document = await Collection
            .Find(md => md.Id == id)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

        return document.AsEntity(factory);
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return Collection.DeleteOneAsync(md => md.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Collection
            .Find(md => md.Id == id)
            .AnyAsync(cancellationToken: cancellationToken);
    }
}
