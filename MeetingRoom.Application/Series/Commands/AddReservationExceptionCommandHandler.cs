using MediatR;
using MeetingRoom.Application.DTOs.ReservationSeries;
using MeetingRoom.Application.Interfaces;
using MeetingRoom.Domain.Entities;

namespace MeetingRoom.Application.Series.Commands;

public class AddReservationExceptionCommandHandler : IRequestHandler<AddReservationExceptionCommand, ReservationExceptionDto>
{
    private readonly IReservationExceptionRepository _exceptionRepository;
    private readonly IReservationSeriesRepository _seriesRepository;

    public AddReservationExceptionCommandHandler(IReservationExceptionRepository exceptionRepository, IReservationSeriesRepository seriesRepository)
    {
        _exceptionRepository = exceptionRepository;
        _seriesRepository = seriesRepository;
    }

    public async Task<ReservationExceptionDto> Handle(AddReservationExceptionCommand request, CancellationToken cancellationToken)
    {
        var series = await _seriesRepository.GetByIdAsync(request.ReservationSeriesId, cancellationToken);
        if (series is null)
            throw new InvalidOperationException("Reservation series not found.");

        var exception = new ReservationException
        {
            ReservationSeriesId = request.ReservationSeriesId,
            ExceptionDate = request.ExceptionDate,
            IsDeleted = false
        };
        var created = await _exceptionRepository.AddAsync(exception, cancellationToken);
        return MapToDto(created);
    }

    private static ReservationExceptionDto MapToDto(ReservationException e)
    {
        return new ReservationExceptionDto
        {
            Id = e.Id,
            ReservationSeriesId = e.ReservationSeriesId,
            ExceptionDate = e.ExceptionDate,
            IsDeleted = e.IsDeleted
        };
    }
}
