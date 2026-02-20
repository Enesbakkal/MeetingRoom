using MeetingRoom.Domain.Entities;

namespace MeetingRoom.Application.Interfaces;

public interface IReservationExceptionRepository
{
    Task<ReservationException?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReservationException>> GetBySeriesIdAsync(int seriesId, CancellationToken cancellationToken = default);
    Task<ReservationException> AddAsync(ReservationException exception, CancellationToken cancellationToken = default);
    Task UpdateAsync(ReservationException exception, CancellationToken cancellationToken = default);
}
