using MediatR;
using MeetingRoom.Application.DTOs.ReservationSeries;
using MeetingRoom.Application.Interfaces;

namespace MeetingRoom.Application.Series.Queries;

public class ListReservationSeriesQueryHandler : IRequestHandler<ListReservationSeriesQuery, IReadOnlyList<ReservationSeriesDto>>
{
    private readonly IReservationSeriesRepository _repository;

    public ListReservationSeriesQueryHandler(IReservationSeriesRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ReservationSeriesDto>> Handle(ListReservationSeriesQuery request, CancellationToken cancellationToken)
    {
        var list = await _repository.GetAllAsync(cancellationToken);
        return list.Select(s => new ReservationSeriesDto
        {
            Id = s.Id,
            Name = s.Name,
            Pattern = s.Pattern,
            StartDate = s.StartDate,
            EndDate = s.EndDate
        }).ToList();
    }
}
