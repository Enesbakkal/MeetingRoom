using MediatR;
using MeetingRoom.Application.DTOs.ReservationSeries;

namespace MeetingRoom.Application.Series.Queries;

public record GetReservationSeriesQuery(int Id) : IRequest<ReservationSeriesDto?>;
