using Meetings.Integrator.Core;
using Meetings.Integrator.Application;
using Meetings.Integrator.Application.Abstractions;
using Meetings.Integrator.Application.Meetings.Commands;
using Meetings.Integrator.Infrastructure;
using Meetings.Integrator.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddCoreConfigurations()
    .AddApplicationConfigurations()
    .AddInfrastructureConfigurations(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection()
   .UseSwagger()
   .UseSwaggerUI()
   .UseErrorHandler();

app.MapPost("/create", async (CreateMicrosoftTeamsMeeting command, ICommandDispatcher commandDispatcher) =>
{
    await commandDispatcher.DispatchAsync(command, CancellationToken.None);
});

app.Run();
