namespace Meetings.Integrator.Core.Exceptions;

public class InvalidTitleException : Exception
{
    internal InvalidTitleException(string message, params object[] args)
        : base(string.Format(message, args))
    {
    }
}
