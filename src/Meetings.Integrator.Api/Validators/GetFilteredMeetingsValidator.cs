using FluentValidation;
using Meetings.Integrator.Application.Meetings.Queries;

namespace Meetings.Integrator.Api.Validators;

public class GetFilteredMeetingsValidator : AbstractValidator<GetFilteredMeetings>
{
    private const int MAX_DAYS = 10;
    public GetFilteredMeetingsValidator()
    {
        RuleFor(x => x.From)
            .NotNull();

        RuleFor(x => x.To)
            .NotNull()
            .GreaterThanOrEqualTo(x => x.From)
            .LessThanOrEqualTo(x => x.From.AddDays(MAX_DAYS));
    }
}