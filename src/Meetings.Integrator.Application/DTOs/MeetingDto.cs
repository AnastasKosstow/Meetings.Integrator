namespace Meetings.Integrator.Application.DTOs;

public class MeetingDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string MeetingLink { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}
