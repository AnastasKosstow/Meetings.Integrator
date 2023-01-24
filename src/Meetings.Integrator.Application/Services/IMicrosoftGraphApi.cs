using Meetings.Integrator.Application.DTOs.Request;
using Meetings.Integrator.Application.DTOs.Response;

namespace Meetings.Integrator.Application.Services;

public interface IMicrosoftGraphApi
{
    Task<CreateMeetingResponse> ScheduleMicrosoftTeamsMeetingAsync(CreateMeetingRequest request, CancellationToken cancellationToken);
}
