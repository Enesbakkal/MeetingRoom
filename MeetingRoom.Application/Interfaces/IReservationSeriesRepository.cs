using MeetingRoom.Domain.Entities;

namespace MeetingRoom.Application.Interfaces;

public interface IReservationSeriesRepository
{
    Task<ReservationSeries?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReservationSeries>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ReservationSeries> AddAsync(ReservationSeries series, CancellationToken cancellationToken = default);
    Task UpdateAsync(ReservationSeries series, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
