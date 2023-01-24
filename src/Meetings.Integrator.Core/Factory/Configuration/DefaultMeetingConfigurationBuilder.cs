using Meetings.Integrator.Core.ValueObjects;

namespace Meetings.Integrator.Core.Factories.Configuration;

internal class DefaultMeetingConfigurationBuilder : IMeetingConfiguration
{
    public Guid Id { get; private set; }
    public Title Title { get; private set; }
    public Link Link { get; private set; }
    public Date Date { get; private set; }


    public IMeetingConfiguration WithId(Guid id)
    {
        if (id == Guid.Empty)
        {
            // throw
        }

        Id = id;
        return this;
    }

    public IMeetingConfiguration WithTitle(string title)
    {
        var titleObject = new Title(title);
        this.Title = titleObject;
        return this;
    }

    public IMeetingConfiguration WithUrl(string url)
    {
        var linkObject = new Link(url);
        this.Link = linkObject;
        return this;
    }

    public IMeetingConfiguration ForDate(DateTime from, DateTime to)
    {
        var dateObject = new Date(from, to);
        this.Date = dateObject;
        return this;
    }
}
