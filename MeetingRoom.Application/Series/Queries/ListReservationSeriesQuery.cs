using MediatR;
using MeetingRoom.Application.DTOs.ReservationSeries;

namespace MeetingRoom.Application.Series.Queries;

public record ListReservationSeriesQuery : IRequest<IReadOnlyList<ReservationSeriesDto>>;
