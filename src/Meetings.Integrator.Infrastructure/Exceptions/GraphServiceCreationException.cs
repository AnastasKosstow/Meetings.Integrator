namespace Meetings.Integrator.Infrastructure.Exceptions;

public class GraphServiceCreationException : Exception
{
    internal GraphServiceCreationException(string message, params object[] args)
        : base(string.Format(message, args))
    {
    }
}