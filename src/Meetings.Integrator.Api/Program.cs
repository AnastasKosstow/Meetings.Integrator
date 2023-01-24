using Meetings.Integrator.Core;
using Meetings.Integrator.Application;
using Meetings.Integrator.Infrastructure;
using Meetings.Integrator.Application.Abstractions;
using Meetings.Integrator.Application.Meetings.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddCoreConfigurations()
    .AddApplicationConfigurations()
    .AddInfrastructureConfigurations(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/create", async (CreateMicrosoftTeamsMeeting command, ICommandDispatcher commandDispatcher) =>
{
    await commandDispatcher.DispatchAsync(command, CancellationToken.None);
});

app.Run();
