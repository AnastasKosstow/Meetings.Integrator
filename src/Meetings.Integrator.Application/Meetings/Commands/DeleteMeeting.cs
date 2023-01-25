using Meetings.Integrator.Application.Abstractions;
using Meetings.Integrator.Application.Exceptions;
using Meetings.Integrator.Core.Abstractions;

namespace Meetings.Integrator.Application.Meetings.Commands;

public record DeleteMeeting(Guid Id)
    : ICommand
{
}


public sealed class DeleteMeetingHandler : ICommandHandler<DeleteMeeting>
{
    private readonly IRepository repository;

    public DeleteMeetingHandler(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task HandleAsync(DeleteMeeting command, CancellationToken cancellationToken)
    {
        if (!await repository.ExistsAsync(command.Id, cancellationToken))
        {
            throw new MeetingMissingException(command.Id.ToString());
        }

        await repository.DeleteAsync(command.Id, cancellationToken);
    }
}