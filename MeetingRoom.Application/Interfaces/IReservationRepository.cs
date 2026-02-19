using MeetingRoom.Domain.Entities;

namespace MeetingRoom.Application.Interfaces;

public interface IReservationRepository
{
    Task<Reservation?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Reservation>> GetListAsync(int? roomId, string? userName, DateTime? from, DateTime? to, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Reservation>> GetConflictsAsync(int roomId, DateTime start, DateTime end, int? excludeReservationId, CancellationToken cancellationToken = default);
    Task<Reservation> AddAsync(Reservation reservation, CancellationToken cancellationToken = default);
    Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken = default);
}
