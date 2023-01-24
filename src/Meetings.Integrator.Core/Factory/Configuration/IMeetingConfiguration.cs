namespace Meetings.Integrator.Core.Factories.Configuration;

public interface IMeetingConfiguration
{
    IMeetingConfiguration WithId(Guid id);
    IMeetingConfiguration WithTitle(string title);
    IMeetingConfiguration WithUrl(string url);
    IMeetingConfiguration ForDate(DateTime from, DateTime to);
}
