using Meetings.Integrator.Core.Exceptions;

namespace Meetings.Integrator.Core.ValueObjects;

public record Link
{
    public string Url { get; private set; }

    public Link(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new InvalidLinkException($"Invalid meeting url value - {url}.");
        }

        Url = url;
    }
}
