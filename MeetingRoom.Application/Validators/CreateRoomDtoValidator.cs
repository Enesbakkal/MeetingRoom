using FluentValidation;
using MeetingRoom.Application.DTOs.Room;

namespace MeetingRoom.Application.Validators;

public class CreateRoomDtoValidator : AbstractValidator<CreateRoomDto>
{
    public CreateRoomDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Capacity).GreaterThan(0);
        RuleFor(x => x.Floor).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Equipment).MaximumLength(500).When(x => x.Equipment != null);
    }
}
