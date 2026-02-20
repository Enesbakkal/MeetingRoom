using MediatR;
using MeetingRoom.Application.DTOs.ReservationSeries;

namespace MeetingRoom.Application.Series.Queries;

public record GetReservationExceptionsQuery(int SeriesId) : IRequest<IReadOnlyList<ReservationExceptionDto>>;
