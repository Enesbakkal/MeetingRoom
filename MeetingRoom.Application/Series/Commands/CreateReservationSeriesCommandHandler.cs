using MediatR;
using MeetingRoom.Application.DTOs.ReservationSeries;
using MeetingRoom.Application.Interfaces;
using MeetingRoom.Domain.Entities;

namespace MeetingRoom.Application.Series.Commands;

public class CreateReservationSeriesCommandHandler : IRequestHandler<CreateReservationSeriesCommand, ReservationSeriesDto>
{
    private readonly IReservationSeriesRepository _repository;

    public CreateReservationSeriesCommandHandler(IReservationSeriesRepository repository)
    {
        _repository = repository;
    }

    public async Task<ReservationSeriesDto> Handle(CreateReservationSeriesCommand request, CancellationToken cancellationToken)
    {
        var series = new ReservationSeries
        {
            Name = request.Name,
            Pattern = request.Pattern,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };
        var created = await _repository.AddAsync(series, cancellationToken);
        return MapToDto(created);
    }

    private static ReservationSeriesDto MapToDto(ReservationSeries s)
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
