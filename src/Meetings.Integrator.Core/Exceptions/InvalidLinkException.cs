namespace Meetings.Integrator.Core.Exceptions;

public class InvalidLinkException : Exception
{
    internal InvalidLinkException(string message, params object[] args)
        : base(string.Format(message, args))
    {
    }
}
