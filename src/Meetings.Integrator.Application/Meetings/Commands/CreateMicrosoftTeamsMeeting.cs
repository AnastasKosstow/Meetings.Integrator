using Meetings.Integrator.Application.Abstractions;
using Meetings.Integrator.Application.DTOs.Request;
using Meetings.Integrator.Application.Exceptions;
using Meetings.Integrator.Application.Services;
using Meetings.Integrator.Core.Abstractions;
using Meetings.Integrator.Core.Factories;

namespace Meetings.Integrator.Application.Meetings.Commands;

public record CreateMicrosoftTeamsMeeting(Guid Id, string AccessToken, string Title, DateTime From, DateTime To) 
    : ICommand
{
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
