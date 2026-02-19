using FluentValidation;
using MeetingRoom.Application.DTOs.Room;

namespace MeetingRoom.Application.Validators;

public class UpdateRoomDtoValidator : AbstractValidator<UpdateRoomDto>
{
    public UpdateRoomDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Capacity).GreaterThan(0);
        RuleFor(x => x.Floor).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Equipment).MaximumLength(500).When(x => x.Equipment != null);
    }
}
