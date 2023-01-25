using Meetings.Integrator.Core;
using Meetings.Integrator.Application;
using Meetings.Integrator.Infrastructure;
using Meetings.Integrator.Api.Middlewares;
using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services
    .AddCoreConfigurations()
    .AddApplicationConfigurations()
    .AddInfrastructureConfigurations(builder.Configuration);

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

app.UseHttpsRedirection()
   .UseSwagger()
   .UseSwaggerUI()
   .UseErrorHandler();

app.MapControllers();
app.Run();
