using Meetings.Integrator.Core.Abstractions;
using Meetings.Integrator.Core.Entities;
using Meetings.Integrator.Core.Factories.Configuration;

namespace Meetings.Integrator.Core.Factories;

public interface IMeetingFactory : IFactory<Meeting>
{
    IMeetingFactory CreateMeeting(Action<IMeetingConfiguration> configAction);
}
