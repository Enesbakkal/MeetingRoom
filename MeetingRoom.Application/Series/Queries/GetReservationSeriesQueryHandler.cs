using MediatR;
using MeetingRoom.Application.DTOs.ReservationSeries;
using MeetingRoom.Application.Interfaces;

namespace MeetingRoom.Application.Series.Queries;

public class GetReservationSeriesQueryHandler : IRequestHandler<GetReservationSeriesQuery, ReservationSeriesDto?>
{
    private readonly IReservationSeriesRepository _repository;

    public GetReservationSeriesQueryHandler(IReservationSeriesRepository repository)
    {
        _repository = repository;
    }

    public async Task<ReservationSeriesDto?> Handle(GetReservationSeriesQuery request, CancellationToken cancellationToken)
    {
        var series = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return series is null ? null : MapToDto(series);
    }

    private static ReservationSeriesDto MapToDto(Domain.Entities.ReservationSeries s)
    {
        return new ReservationSeriesDto
        {
            Id = s.Id,
            Name = s.Name,
            Pattern = s.Pattern,
            StartDate = s.StartDate,
            EndDate = s.EndDate
        };
    }
}
