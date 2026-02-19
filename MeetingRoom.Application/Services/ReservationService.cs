using MeetingRoom.Application.DTOs.Reservation;
using MeetingRoom.Application.Interfaces;
using MeetingRoom.Domain.Entities;

namespace MeetingRoom.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _repository;

    public ReservationService(IReservationRepository repository)
    {
        _repository = repository;
    }

    public async Task<ReservationDto> CreateAsync(CreateReservationDto dto, CancellationToken cancellationToken = default)
    {
        var conflicts = await _repository.GetConflictsAsync(dto.RoomId, dto.StartTime, dto.EndTime, null, cancellationToken);
        if (conflicts.Count > 0)
            throw new InvalidOperationException("Room has conflicting reservations for the given time range.");

        var reservation = new Reservation
        {
            RoomId = dto.RoomId,
            UserName = dto.UserName,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            IsCanceled = false,
            CreatedAt = DateTime.UtcNow
        };
        var created = await _repository.AddAsync(reservation, cancellationToken);
        return MapToDto(created);
    }

    public async Task<ReservationDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var r = await _repository.GetByIdAsync(id, cancellationToken);
        return r is null ? null : MapToDto(r);
    }

    public async Task<IReadOnlyList<ReservationDto>> GetListAsync(ReservationFilterDto filter, CancellationToken cancellationToken = default)
    {
        var list = await _repository.GetListAsync(filter.RoomId, filter.UserName, filter.From, filter.To, cancellationToken);
        return list.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<ReservationDto>> GetConflictsAsync(int roomId, DateTime start, DateTime end, int? excludeReservationId, CancellationToken cancellationToken = default)
    {
        var list = await _repository.GetConflictsAsync(roomId, start, end, excludeReservationId, cancellationToken);
        return list.Select(MapToDto).ToList();
    }

    public async Task<ReservationDto?> UpdateAsync(int id, UpdateReservationDto dto, CancellationToken cancellationToken = default)
    {
        var r = await _repository.GetByIdAsync(id, cancellationToken);
        if (r is null || r.IsCanceled) return null;

        var conflicts = await _repository.GetConflictsAsync(r.RoomId, dto.StartTime, dto.EndTime, id, cancellationToken);
        if (conflicts.Count > 0)
            throw new InvalidOperationException("Room has conflicting reservations for the given time range.");

        r.StartTime = dto.StartTime;
        r.EndTime = dto.EndTime;
        await _repository.UpdateAsync(r, cancellationToken);
        return MapToDto(r);
    }

    public async Task<bool> CancelAsync(int id, CancellationToken cancellationToken = default)
    {
        var r = await _repository.GetByIdAsync(id, cancellationToken);
        if (r is null || r.IsCanceled) return false;
        r.IsCanceled = true;
        await _repository.UpdateAsync(r, cancellationToken);
        return true;
    }

    private static ReservationDto MapToDto(Reservation r)
    {
        return new ReservationDto
        {
            Id = r.Id,
            RoomId = r.RoomId,
            UserName = r.UserName,
            StartTime = r.StartTime,
            EndTime = r.EndTime,
            IsCanceled = r.IsCanceled,
            CreatedAt = r.CreatedAt
        };
    }
}
