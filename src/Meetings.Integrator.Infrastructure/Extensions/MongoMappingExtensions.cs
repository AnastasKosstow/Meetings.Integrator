using Meetings.Integrator.Core.Entities;
using Meetings.Integrator.Core.Factories;
using Meetings.Integrator.Infrastructure.Persistence.Documents;

namespace Meetings.Integrator.Infrastructure.Extensions;

internal static class MongoMappingExtensions
{
    internal static Meeting AsEntity(this MeetingDocument document, IMeetingFactory factory)
        =>
        factory.CreateMeeting(config =>
        {
            config
                .WithId(document.Id)
                .WithTitle(document.Title)
                .WithUrl(document.MeetingLink)
                .ForDate(document.From, document.To);
        }).Build();

    internal static MeetingDocument AsDocument(this Meeting meeting)
        =>
        new()
        {
            Id = meeting.Id,
            Title = meeting.Title.MeetingTitle,
            MeetingLink = meeting.MeetingLink.Url,
            From = meeting.Date.From,
            To = meeting.Date.To
        };
}
