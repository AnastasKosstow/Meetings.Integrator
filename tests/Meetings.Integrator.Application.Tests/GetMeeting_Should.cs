using Meetings.Integrator.Application.Exceptions;
using Meetings.Integrator.Application.Meetings.Queries;
using Meetings.Integrator.Core.Abstractions;
using Meetings.Integrator.Core.Entities;

namespace Meetings.Integrator.Application.Tests;

public class GetMeeting_Should
{
    [Test]
    public void Throws_MeetingMissingException_When_Meeting_Missing()
    {
        var query = new GetMeeting(It.IsAny<Guid>());

        repository
            .Setup(repository => repository.GetAsync(It.IsAny<Guid>(), CancellationToken.None))
            .ReturnsAsync(It.IsAny<Meeting>());

        Assert.ThrowsAsync<MeetingMissingException>(async () => await sut.HandleAsync(query, CancellationToken.None));
    }


    #region ARRANGE

    protected readonly Mock<IRepository> repository = new();

    private readonly GetMeetingHandler sut;

    public GetMeeting_Should()
    {
        sut = new GetMeetingHandler(repository.Object);
    }

    #endregion
}
