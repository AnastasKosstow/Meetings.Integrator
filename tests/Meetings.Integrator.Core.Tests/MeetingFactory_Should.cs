using Meetings.Integrator.Core.Exceptions;
using Meetings.Integrator.Core.Factories;

namespace Meetings.Integrator.Core.Tests;

public class MeetingFactory_Should
{
    [Test]
    public void Throws_InvalidAggregateIdException_When_Url_Is_Not_Correct()
    {
        Assert.Throws<InvalidAggregateIdException>(() =>
            factory.CreateMeeting(config =>
            {
                config
                    .WithId(Guid.Empty)
                    .WithTitle("title")
                    .WithUrl("url")
                    .ForDate(DateTime.Now, DateTime.Now);
            }));
    }

    [Test]
    public void Throws_InvalidTitleException_When_Title_Is_Not_Correct()
    {
        Assert.Throws<InvalidTitleException>(() => 
            factory.CreateMeeting(config =>
            {
                config
                    .WithId(Guid.NewGuid())
                    .WithTitle(string.Empty)
                    .WithUrl("url")
                    .ForDate(DateTime.Now, DateTime.Now);
            }));
    }

    [Test]
    public void Throws_InvalidLinkException_When_Url_Is_Not_Correct()
    {
        Assert.Throws<InvalidLinkException>(() =>
            factory.CreateMeeting(config =>
            {
                config
                    .WithId(Guid.NewGuid())
                    .WithTitle("title")
                    .WithUrl(string.Empty)
                    .ForDate(DateTime.Now, DateTime.Now);
            }));
    }


    #region ARRANGE

    private readonly IMeetingFactory factory;

    public MeetingFactory_Should()
    {
        factory = new MeetingFactory();
    }

    #endregion
}
