namespace Meetings.Integrator.Infrastructure.Exceptions;

public class MicrosoftGraphApiRequestException : Exception
{
    internal MicrosoftGraphApiRequestException(string message, params object[] args)
        : base(string.Format(message, args))
    {
    }
}