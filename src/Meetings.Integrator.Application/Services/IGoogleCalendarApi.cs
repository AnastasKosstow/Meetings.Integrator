using Meetings.Integrator.Application.DTOs.Request;
using Meetings.Integrator.Application.DTOs.Response;

namespace Meetings.Integrator.Application.Services;

public interface IGoogleCalendarApi
{
    Task<CreateMeetingResponse> ScheduleGoogleHangoutMeetingAsync(CreateMeetingRequest request, CancellationToken cancellationToken);
}
