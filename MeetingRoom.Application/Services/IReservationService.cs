using MeetingRoom.Application.DTOs.Reservation;

namespace MeetingRoom.Application.Services;

public interface IReservationService
{
    Task<ReservationDto> CreateAsync(CreateReservationDto dto, CancellationToken cancellationToken = default);
    Task<ReservationDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReservationDto>> GetListAsync(ReservationFilterDto filter, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReservationDto>> GetConflictsAsync(int roomId, DateTime start, DateTime end, int? excludeReservationId, CancellationToken cancellationToken = default);
    Task<ReservationDto?> UpdateAsync(int id, UpdateReservationDto dto, CancellationToken cancellationToken = default);
    Task<bool> CancelAsync(int id, CancellationToken cancellationToken = default);
}
