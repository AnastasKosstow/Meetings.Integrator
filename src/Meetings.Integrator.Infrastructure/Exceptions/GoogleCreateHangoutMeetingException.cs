namespace Meetings.Integrator.Infrastructure.Exceptions;

public class GoogleCreateHangoutMeetingException : Exception
{
    internal GoogleCreateHangoutMeetingException(string message, params object[] args)
        : base(string.Format(message, args))
    {
    }
}