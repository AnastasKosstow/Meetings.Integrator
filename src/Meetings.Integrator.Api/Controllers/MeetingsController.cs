using Meetings.Integrator.Application.Abstractions;
using Meetings.Integrator.Application.DTOs;
using Meetings.Integrator.Application.Meetings.Commands;
using Meetings.Integrator.Application.Meetings.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.Integrator.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetingsController : ControllerBase
{
    private readonly ICommandDispatcher commandDispatcher;
    private readonly IQueryDispatcher queryDispatcher;

    public MeetingsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        this.commandDispatcher = commandDispatcher;
        this.queryDispatcher = queryDispatcher;
    }

    [HttpPost(nameof(Create))]
    public async Task<ActionResult> Create(CreateMeeting command, CancellationToken cancellationToken)
    {
        await commandDispatcher.DispatchAsync(command, cancellationToken);
        return Ok();
    }

    [HttpPost(nameof(Delete))]
    public async Task<ActionResult> Delete(DeleteMeeting command, CancellationToken cancellationToken)
    {
        await commandDispatcher.DispatchAsync(command, cancellationToken);
        return Ok();
    }

    [HttpGet(nameof(Get))]
    public async Task<ActionResult<MeetingDto>> Get([FromQuery] GetMeeting query, CancellationToken cancellationToken)
    {
        var meeting = await queryDispatcher.DispatchAsync(query, cancellationToken);
        return Ok(meeting);
    }

    [HttpGet(nameof(GetFor))]
    public async Task<ActionResult<IEnumerable<MeetingDto>>> GetFor([FromQuery] GetFilteredMeetings query, CancellationToken cancellationToken)
    {
        var meetings = await queryDispatcher.DispatchAsync(query, cancellationToken);
        return Ok(meetings);
    }
}
