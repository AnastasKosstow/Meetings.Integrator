namespace Meetings.Integrator.Core.Exceptions;

public class InvalidAggregateIdException : Exception
{
    internal InvalidAggregateIdException(string message, params object[] args)
        : base(string.Format(message, args))
    {
    }
}
