using Meetings.Integrator.Application.DTOs.Request;
using Meetings.Integrator.Application.DTOs.Response;
using Meetings.Integrator.Application.Services;
using Meetings.Integrator.Infrastructure.Exceptions;
using Meetings.Integrator.Infrastructure.Services.Microsoft.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace Meetings.Integrator.Infrastructure.Services.Microsoft;

public sealed class MicrosoftGraphApi : IMicrosoftGraphApi
{
    private readonly MicrosoftGraphApiSettings settings;

    public MicrosoftGraphApi(IOptions<MicrosoftGraphApiSettings> options)
    {
        this.settings = options.Value;
    }

    public async Task<CreateMeetingResponse> ScheduleMicrosoftTeamsMeetingAsync(CreateMeetingRequest request, CancellationToken cancellationToken)
    {
        var graphServiceClient = CreateGraphServiceClient(request.AccessToken);

        try
        {
            var externalId = Guid.NewGuid().ToString();

            var onlineMeeting = await graphServiceClient.Me.OnlineMeetings
                .CreateOrGet(
                    externalId: externalId,
                    subject: request.Title,
                    startDateTime: request.From,
                    endDateTime: request.To)
                .Request()
                .WithMaxRetry(3)
                .PostAsync(cancellationToken);

            var response = new CreateMeetingResponse(onlineMeeting.JoinWebUrl);
            return response;
        }
        catch
        {
            throw new MicrosoftGraphApiRequestException("Unable to schedule a Microsoft Teams meeting.");
        }
    }

    private GraphServiceClient CreateGraphServiceClient(string accessToken)
    {
        try
        {
            var clientAppBuilder = ConfidentialClientApplicationBuilder
                .Create(settings.ClientId)
                .WithTenantId(settings.TenantId)
                .WithClientSecret(settings.ClientSecret)
                .Build();

            var assertion = new UserAssertion(accessToken);
            var authProvider = new DelegateAuthenticationProvider(async (request) =>
            {
                var result = await clientAppBuilder.AcquireTokenOnBehalfOf(settings.Scopes, assertion).ExecuteAsync();
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.AccessToken);
            });

            var graphServiceClient = new GraphServiceClient(authProvider);
            return graphServiceClient;
        }
        catch
        {
            throw new GraphServiceCreationException("An error occurred while attempting to create GraphServiceClient.");
        }
    }
}
