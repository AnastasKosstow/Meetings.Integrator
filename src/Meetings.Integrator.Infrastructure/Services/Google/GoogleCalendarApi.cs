using Meetings.Integrator.Application.DTOs.Request;
using Meetings.Integrator.Application.DTOs.Response;
using Meetings.Integrator.Application.Services;
using Meetings.Integrator.Infrastructure.Exceptions;
using Meetings.Integrator.Infrastructure.Services.Google.Settings;
using Microsoft.Extensions.Options;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System.IdentityModel.Tokens.Jwt;

public sealed class GoogleCalendarApi : IGoogleCalendarApi
{
    private const string CALENDAR_ID = "primary";
    private const string CONFERENCE_TYPE = "hangoutsMeet";

    private readonly GoogleCalendarApiSettings settings;

    public GoogleCalendarApi(IOptions<GoogleCalendarApiSettings> options)
    {
        this.settings = options.Value;
    }

    public async Task<CreateMeetingResponse> ScheduleGoogleHangoutMeetingAsync(CreateMeetingRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var calendarService = CreateGoogleCalendarService(request.AccessToken);

            var calendarEvent = new Event()
            {
                Summary = request.Title,
                Start = new EventDateTime() { DateTime = request.From },
                End = new EventDateTime() { DateTime = request.To },

                AnyoneCanAddSelf = true,
                ConferenceData = new()
                {
                    CreateRequest = new()
                    {
                        ConferenceSolutionKey = new() { Type = CONFERENCE_TYPE },
                        RequestId = Guid.NewGuid().ToString()
                    }
                }
            };

            var eventRequestGoogle = calendarService
                .Events
                .Insert(calendarEvent, CALENDAR_ID);
            eventRequestGoogle.ConferenceDataVersion = 1;

            var eventResponse = await eventRequestGoogle.ExecuteAsync(cancellationToken);

            var response = new CreateMeetingResponse(eventResponse.HangoutLink);
            return response;
        }
        catch
        {
            throw new GoogleCreateHangoutMeetingException("Unable to schedule a Google Hangout meeting.");
        }
    }

    private CalendarService CreateGoogleCalendarService(string accessToken)
    {
        // We need user email for impersonating the user and create events and meetings on behalf of the user.

        #region Just for testing purposes

        // In production environment use IUserContext service of some kind that read values from jwt.

        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = tokenHandler
            .ReadJwtToken(accessToken)
            .Claims;

        var userEmail = claims.FirstOrDefault(x => x.Type == "email")?.Value;

        #endregion

        var credentials = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(settings.ClientEmail)
        {
            Scopes = new[] { CalendarService.Scope.Calendar },
            User = userEmail
        }.FromPrivateKey(settings.PrivateKey));

        // Create Google Calendar API service.
        var calendarService = new CalendarService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credentials,
            ApplicationName = "mon"
        });

        return calendarService;
    }
}
