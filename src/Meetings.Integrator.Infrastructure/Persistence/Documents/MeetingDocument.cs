namespace Meetings.Integrator.Infrastructure.Persistence.Documents;

internal sealed class MeetingDocument
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string MeetingLink { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}
