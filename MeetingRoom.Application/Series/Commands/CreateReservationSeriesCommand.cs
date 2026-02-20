using MediatR;
using MeetingRoom.Application.DTOs.ReservationSeries;

namespace MeetingRoom.Application.Series.Commands;

public record CreateReservationSeriesCommand(string? Name, string? Pattern, DateTime? StartDate, DateTime? EndDate) : IRequest<ReservationSeriesDto>;
