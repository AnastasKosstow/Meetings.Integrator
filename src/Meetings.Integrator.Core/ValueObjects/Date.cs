namespace Meetings.Integrator.Core.ValueObjects;

public record Date
{
    public DateTime From { get; private set; }
    public DateTime To { get; private set; }

    public Date(DateTime from, DateTime to)
    {
        From = from;
        To = to;
    }
}
