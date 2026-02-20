using FluentValidation;
using MeetingRoom.Application.Series.Commands;

namespace MeetingRoom.Application.Series.Validators;

public class CreateReservationSeriesCommandValidator : AbstractValidator<CreateReservationSeriesCommand>
{
    public CreateReservationSeriesCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(200).When(x => x.Name != null);
        RuleFor(x => x.Pattern).MaximumLength(50).When(x => x.Pattern != null);
        RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate).When(x => x.StartDate.HasValue && x.EndDate.HasValue).WithMessage("EndDate must be on or after StartDate.");
    }
}
