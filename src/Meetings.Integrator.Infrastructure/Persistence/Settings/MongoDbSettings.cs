namespace Meetings.Integrator.Infrastructure.Persistence.Settings;

public sealed class MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string Database { get; set; }
    public bool Seed { get; set; }
}
