using Meetings.Integrator.Application.Abstractions;
using Meetings.Integrator.Application.DTOs.Request;
using Meetings.Integrator.Application.Enums;
using Meetings.Integrator.Application.Exceptions;
using Meetings.Integrator.Application.Services;
using Meetings.Integrator.Core.Abstractions;
using Meetings.Integrator.Core.Factories;

namespace Meetings.Integrator.Application.Meetings.Commands;

public record CreateMeeting(Guid Id, string AccessToken, string Title, DateTime From, DateTime To, ExternalSystem ExternalSystem) 
    : ICommand
{
}

public sealed class CreateMeetingHandler : ICommandHandler<CreateMeeting>
{
    private readonly IRepository repository;
    private readonly IMeetingFactory meetingsFactory;
    private readonly IMicrosoftGraphApi microsoftGraphApi;
    private readonly IGoogleCalendarApi googleCalendarApi;

    public CreateMeetingHandler(
        IRepository repository,
        IMeetingFactory meetingsFactory,
        IMicrosoftGraphApi microsoftGraphApi,
        IGoogleCalendarApi googleCalendarApi)
    {
        this.repository = repository;
        this.meetingsFactory = meetingsFactory;
        this.microsoftGraphApi = microsoftGraphApi;
        this.googleCalendarApi = googleCalendarApi;
    }

    public async Task HandleAsync(CreateMeeting command, CancellationToken cancellationToken)
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

        var teamsResponse = command.ExternalSystem switch
        {
            ExternalSystem.MicrosoftTeams => await microsoftGraphApi.ScheduleMicrosoftTeamsMeetingAsync(createMeetingRequest, cancellationToken),
            ExternalSystem.GoogleHangout => await googleCalendarApi.ScheduleGoogleHangoutMeetingAsync(createMeetingRequest, cancellationToken),

            _ => throw new NotImplementedException()
        };

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
