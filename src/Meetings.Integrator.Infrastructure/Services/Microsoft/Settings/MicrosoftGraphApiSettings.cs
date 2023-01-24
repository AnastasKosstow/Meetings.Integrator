namespace Meetings.Integrator.Infrastructure.Services.Microsoft.Settings;

public sealed class MicrosoftGraphApiSettings
{
    public string TenantId { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string[] Scopes { get; set; }
}