using Microsoft.Extensions.Configuration;

namespace Meetings.Integrator.Infrastructure.Extensions;

public static class IConfigurationExtensions
{
    public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionName)
        where TOptions : new()
    {
        var options = new TOptions();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
}
