using Meetings.Integrator.Application.Abstractions;
using Meetings.Integrator.Application.DTOs.Request;
using Meetings.Integrator.Application.Exceptions;
using Meetings.Integrator.Application.Services;
using Meetings.Integrator.Core.Abstractions;
using Meetings.Integrator.Core.Factories;

namespace Meetings.Integrator.Application.Meetings.Commands;

public record CreateMicrosoftTeamsMeeting : ICommand
{
    public Guid Id { get; }
    public string AccessToken { get; }
    public string Title { get; }
    public DateTime From { get; }
    public DateTime To { get; }

    public CreateMicrosoftTeamsMeeting(Guid id, string accessToken, string title, DateTime from, DateTime to)
        =>
        (Id, AccessToken, Title, From, To) = (id, accessToken, title, from, to);
}

public sealed class CreateMicrosoftTeamsMeetingHandler : ICommandHandler<CreateMicrosoftTeamsMeeting>
{
    private readonly IMeetingFactory meetingsFactory;
    private readonly IMicrosoftGraphApi microsoftGraphApi;
    private readonly IRepository repository;

    public CreateMicrosoftTeamsMeetingHandler(
        IMeetingFactory meetingsFactory,
        IMicrosoftGraphApi microsoftGraphApi,
        IRepository repository)
    {
        this.meetingsFactory = meetingsFactory;
        this.microsoftGraphApi = microsoftGraphApi;
        this.repository = repository;
    }

    public async Task HandleAsync(CreateMicrosoftTeamsMeeting command, CancellationToken cancellationToken)
    {
        // if (meeting is { })
        if (await repository.ExistsAsync(command.Id, cancellationToken))
        {
            throw new MeetingAlreadyExistsException(command.Id.ToString());
        }

        var createMeetingRequest = new CreateMeetingRequest(
            command.AccessToken,
            command.Title,
            command.From,
            command.To);

        var teamsResponse = await microsoftGraphApi.ScheduleMicrosoftTeamsMeetingAsync(createMeetingRequest, cancellationToken);

        var meeting = meetingsFactory.CreateMeeting(config =>
        {
            config
                .WithId(command.Id)
                .WithTitle(command.Title)
                .WithUrl(teamsResponse.MeetingUrl)
                .ForDate(command.From, command.To);
        }).Build();

        await repository.AddAsync(meeting, cancellationToken);
    }
}
