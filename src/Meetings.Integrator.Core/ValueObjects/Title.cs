using Meetings.Integrator.Core.Exceptions;

namespace Meetings.Integrator.Core.ValueObjects;

public record Title
{
    public string MeetingLink { get; private set; }

    public Title(string meetingLink)
    {
        if (string.IsNullOrWhiteSpace(meetingLink))
        {
            throw new InvalidTitleException($"Invalid title value - {meetingLink}.");
        }

        MeetingLink = meetingLink;
    }
}
