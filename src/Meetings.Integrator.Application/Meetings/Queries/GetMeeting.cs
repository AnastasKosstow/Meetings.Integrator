using Meetings.Integrator.Application.Abstractions;
using Meetings.Integrator.Application.DTOs;
using Meetings.Integrator.Core.Abstractions;

namespace Meetings.Integrator.Application.Meetings.Queries;

public record GetMeeting(Guid Id)
    : IQuery<MeetingDto>
{
}

public sealed class GetMeetingHandler : IQueryHandler<GetMeeting, MeetingDto>
{
    private readonly IRepository repository;

    public GetMeetingHandler(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task<MeetingDto> HandleAsync(GetMeeting query, CancellationToken cancellationToken)
    {
        var meeting = await repository.GetAsync(query.Id, cancellationToken);

        return new()
        {
            Id = meeting.Id,
            Title = meeting.Title.MeetingTitle,
            MeetingLink = meeting.MeetingLink.Url,
            From = meeting.Date.From,
            To = meeting.Date.To
        };
    }
}
