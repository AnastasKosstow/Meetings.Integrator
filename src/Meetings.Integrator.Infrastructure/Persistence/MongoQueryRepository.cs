using Meetings.Integrator.Application.Abstractions;
using Meetings.Integrator.Core.Entities;
using Meetings.Integrator.Core.Factories;
using Meetings.Integrator.Infrastructure.Extensions;
using Meetings.Integrator.Infrastructure.Persistence.Documents;
using MongoDB.Driver;

namespace Meetings.Integrator.Infrastructure.Persistence;

internal class MongoQueryRepository : IQueryRepository
{
    private readonly IMeetingFactory factory;
    private readonly IMongoCollection<MeetingDocument> Collection;

    public MongoQueryRepository(IMeetingFactory factory, IMongoDatabase database)
    {
        this.factory = factory;
        Collection = database.GetCollection<MeetingDocument>("meetings");
    }

    public async Task<IEnumerable<Meeting>> GetByDateAsync(DateTime from, DateTime to, CancellationToken cancellationToken)
    {
        var documents = await Collection
            .Find(md => md.From >= from && md.To <= to)
            .ToListAsync(cancellationToken);

        var meetings = documents.Select(document => document.AsEntity(factory));
        return meetings;
    }
}

