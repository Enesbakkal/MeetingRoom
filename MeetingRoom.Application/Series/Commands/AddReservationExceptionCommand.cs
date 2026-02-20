using MediatR;
using MeetingRoom.Application.DTOs.ReservationSeries;

namespace MeetingRoom.Application.Series.Commands;

public record AddReservationExceptionCommand(int ReservationSeriesId, DateTime? ExceptionDate) : IRequest<ReservationExceptionDto>;
