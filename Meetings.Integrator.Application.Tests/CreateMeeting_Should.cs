using Meetings.Integrator.Application.Enums;
using Meetings.Integrator.Application.Exceptions;
using Meetings.Integrator.Application.Meetings.Commands;
using Meetings.Integrator.Application.Services;
using Meetings.Integrator.Core.Abstractions;
using Meetings.Integrator.Core.Factories;

namespace Meetings.Integrator.Application.Tests;

public class CreateMeeting_Should
{
    [Test]
    public void Throws_MeetingAlreadyExistsException_When_Meeting_With_same_Name_Already_Exists()
    {
        var command = new CreateMeeting(
            It.IsAny<Guid>(), 
            It.IsAny<string>(), 
            It.IsAny<string>(), 
            It.IsAny<DateTime>(), 
            It.IsAny<DateTime>(), 
            default);

        repository
            .Setup(repository => repository.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None))
            .ReturnsAsync(true);

        Assert.ThrowsAsync<MeetingAlreadyExistsException>(async () => await sut.HandleAsync(command, CancellationToken.None));
    }

    [Test]
    public void Throws_NotImplementedException_When_External_System_Is_Not_Found()
    {
        var command = new CreateMeeting(
            It.IsAny<Guid>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<DateTime>(),
            It.IsAny<DateTime>(),
            (ExternalSystem)3);

        repository
            .Setup(repository => repository.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None))
            .ReturnsAsync(false);

        Assert.ThrowsAsync<NotImplementedException>(async () => await sut.HandleAsync(command, CancellationToken.None));
    }


    #region ARRANGE

    protected readonly Mock<IRepository> repository = new();
    protected readonly Mock<IMeetingFactory> meetingsFactory = new();
    protected readonly Mock<IMicrosoftGraphApi> microsoftGraphApi = new();
    protected readonly Mock<IGoogleCalendarApi> googleCalendarApi = new();

    private readonly CreateMeetingHandler sut;

    public CreateMeeting_Should()
    {
        sut = new CreateMeetingHandler(
            repository.Object,
            meetingsFactory.Object,
            microsoftGraphApi.Object,
            googleCalendarApi.Object);
    }

    #endregion
}
