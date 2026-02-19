using FluentValidation;
using MeetingRoom.Application.DTOs.Reservation;

namespace MeetingRoom.Application.Validators;

public class CreateReservationDtoValidator : AbstractValidator<CreateReservationDto>
{
    public CreateReservationDtoValidator()
    {
        RuleFor(x => x.RoomId).GreaterThan(0);
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).WithMessage("EndTime must be after StartTime.");
    }
}
