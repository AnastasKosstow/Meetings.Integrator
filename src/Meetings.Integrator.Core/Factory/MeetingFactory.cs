using Meetings.Integrator.Core.Entities;
using Meetings.Integrator.Core.Factories.Configuration;

namespace Meetings.Integrator.Core.Factories;

internal class MeetingFactory : IMeetingFactory
{
    private DefaultMeetingConfigurationBuilder configuration;

    public IMeetingFactory CreateMeeting(Action<IMeetingConfiguration> configAction)
    {
        var configBuilder = new DefaultMeetingConfigurationBuilder();
        configAction?.Invoke(configBuilder);

        this.configuration = configBuilder;
        return this;
    }

    public Meeting Build()
        =>
        new(configuration.Id,
            configuration.Title,
            configuration.Link,
            configuration.Date);
}
