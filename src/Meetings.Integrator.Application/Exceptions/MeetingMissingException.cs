namespace Meetings.Integrator.Application.Exceptions;

public class MeetingMissingException : Exception
{
    internal MeetingMissingException(string message, params object[] args)
        : base(string.Format(message, args))
    {
    }
}
