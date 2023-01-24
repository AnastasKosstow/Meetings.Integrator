namespace Meetings.Integrator.Application.Exceptions;

public class MeetingAlreadyExistsException : Exception
{
    internal MeetingAlreadyExistsException(string message, params object[] args)
        : base(string.Format(message, args))
    {
    }
}
