using Meetings.Integrator.Core.Exceptions;

namespace Meetings.Integrator.Core.ValueObjects;

public record Title
{
    public string MeetingTitle { get; private set; }

    public Title(string meetingLink)
    {
        if (string.IsNullOrWhiteSpace(meetingLink))
        {
            throw new InvalidTitleException("Title must be provided");
        }

        MeetingTitle = meetingLink;
    }
}
