using Meetings.Integrator.Application.Exceptions;
using Meetings.Integrator.Application.Meetings.Commands;
using Meetings.Integrator.Core.Abstractions;

namespace Meetings.Integrator.Application.Tests;

public class DeleteMeeting_Should
{
    [Test]
    public void Throws_MeetingMissingException_When_Meeting_Missing()
    {
        var command = new DeleteMeeting(It.IsAny<Guid>());

        repository
            .Setup(repository => repository.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None))
            .ReturnsAsync(false);

        Assert.ThrowsAsync<MeetingMissingException>(async () => await sut.HandleAsync(command, CancellationToken.None));
    }


    #region ARRANGE

    protected readonly Mock<IRepository> repository = new();

    private readonly DeleteMeetingHandler sut;

    public DeleteMeeting_Should()
    {
        sut = new DeleteMeetingHandler(repository.Object);
    }

    #endregion
}
