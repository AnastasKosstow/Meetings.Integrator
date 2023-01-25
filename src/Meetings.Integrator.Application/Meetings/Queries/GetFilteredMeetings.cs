using Meetings.Integrator.Application.Abstractions;
using Meetings.Integrator.Application.DTOs;

namespace Meetings.Integrator.Application.Meetings.Queries;

public record GetFilteredMeetings(DateTime From, DateTime To) 
    : IQuery<IEnumerable<MeetingDto>>
{
}

public sealed class GetFilteredMeetingsHandler : IQueryHandler<GetFilteredMeetings, IEnumerable<MeetingDto>>
{
    private readonly IQueryRepository queryRepository;

    public GetFilteredMeetingsHandler(IQueryRepository queryRepository)
    {
        this.queryRepository = queryRepository;
    }

    public async Task<IEnumerable<MeetingDto>> HandleAsync(GetFilteredMeetings query, CancellationToken cancellationToken)
    {
        var meetings = await queryRepository.GetByDateAsync(query.From, query.To, cancellationToken);

        return meetings.Select(meeting =>
            new MeetingDto()
            {
                Id = meeting.Id,
                Title = meeting.Title.MeetingTitle,
                MeetingLink = meeting.MeetingLink.Url,
                From = meeting.Date.From,
                To = meeting.Date.To
            });
    }
}
