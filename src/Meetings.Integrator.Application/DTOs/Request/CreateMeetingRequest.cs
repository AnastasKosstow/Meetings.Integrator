namespace Meetings.Integrator.Application.DTOs.Request;

public record CreateMeetingRequest(
    string AccessToken,
    string Title,
    DateTime From,
    DateTime To);
