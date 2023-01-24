using Meetings.Integrator.Core.Abstractions;
using Meetings.Integrator.Core.ValueObjects;

namespace Meetings.Integrator.Core.Entities;

public sealed class Meeting : IAggregateRoot
{
    public Guid Id { get; private set; }
    public Title Title { get; private set; }
    public Link MeetingLink { get; private set; }
    public Date Date { get; private set; }

    internal Meeting(Guid id, Title title, Link meetingLink, Date date)
    {
        Id = id;
        Title = title;
        MeetingLink = meetingLink;
        Date = date;
    }
}
