using MediatR;
using MeetingRoom.Application.DTOs.ReservationSeries;
using MeetingRoom.Application.Interfaces;

namespace MeetingRoom.Application.Series.Queries;

public class GetReservationExceptionsQueryHandler : IRequestHandler<GetReservationExceptionsQuery, IReadOnlyList<ReservationExceptionDto>>
{
    private readonly IReservationExceptionRepository _repository;

    public GetReservationExceptionsQueryHandler(IReservationExceptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ReservationExceptionDto>> Handle(GetReservationExceptionsQuery request, CancellationToken cancellationToken)
    {
        var list = await _repository.GetBySeriesIdAsync(request.SeriesId, cancellationToken);
        return list.Select(e => new ReservationExceptionDto
        {
            Id = e.Id,
            ReservationSeriesId = e.ReservationSeriesId,
            ExceptionDate = e.ExceptionDate,
            IsDeleted = e.IsDeleted
        }).ToList();
    }
}
