using FluentValidation;
using MeetingRoom.Application.DTOs.Reservation;

namespace MeetingRoom.Application.Validators;

public class UpdateReservationDtoValidator : AbstractValidator<UpdateReservationDto>
{
    public UpdateReservationDtoValidator()
    {
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).WithMessage("EndTime must be after StartTime.");
    }
}
