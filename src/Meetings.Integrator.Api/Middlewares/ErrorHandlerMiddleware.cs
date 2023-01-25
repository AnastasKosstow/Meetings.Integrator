using Meetings.Integrator.Core.Exceptions;
using Meetings.Integrator.Application.Exceptions;
using Meetings.Integrator.Infrastructure.Exceptions;
using System.Net;

namespace Meetings.Integrator.Api.Middlewares;

public static class ErrorHandlerExtensions
{
    public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        =>
        builder.UseMiddleware<ErrorHandlerMiddleware>();
}


internal sealed class ErrorHandlerMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ErrorHandlerMiddleware> logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
            await HandleErrorAsync(context, exception);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        var exceptionResponse = MapException(exception);

        context.Response.StatusCode = (int)exceptionResponse.StatusCode;
        context.Response.ContentType = "application/json";

        var response = exception?.Message;
        if (response is null)
        {
            await context.Response.WriteAsync(string.Empty);
            return;
        }

        await context.Response.WriteAsync(exception?.Message ?? string.Empty);
    }

    private (object Response, HttpStatusCode StatusCode) MapException(Exception exception)
        =>
        exception switch
        {
            InvalidLinkException e => (e.Message, HttpStatusCode.BadRequest),
            InvalidTitleException e => (e.Message, HttpStatusCode.BadRequest),
            MeetingAlreadyExistsException e => (e.Message, HttpStatusCode.BadRequest),
            GraphServiceCreationException e => (e.Message, HttpStatusCode.BadRequest),
            MicrosoftGraphApiRequestException e => (e.Message, HttpStatusCode.BadRequest),

            _ => (exception.Message, HttpStatusCode.InternalServerError)
        };
}
