using Microsoft.EntityFrameworkCore;
using MeetingRoom.Application.Interfaces;
using MeetingRoom.Domain.Entities;
using MeetingRoom.Infrastructure.Data;

namespace MeetingRoom.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _db;

    public ReservationRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Reservation?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _db.Reservations.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IReadOnlyList<Reservation>> GetListAsync(int? roomId, string? userName, DateTime? from, DateTime? to, CancellationToken cancellationToken = default)
    {
        var q = _db.Reservations.AsQueryable();
        if (roomId.HasValue) q = q.Where(x => x.RoomId == roomId.Value);
        if (!string.IsNullOrWhiteSpace(userName)) q = q.Where(x => x.UserName == userName);
        if (from.HasValue) q = q.Where(x => x.EndTime >= from.Value);
        if (to.HasValue) q = q.Where(x => x.StartTime <= to.Value);
        return await q.OrderBy(x => x.StartTime).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Reservation>> GetConflictsAsync(int roomId, DateTime start, DateTime end, int? excludeReservationId, CancellationToken cancellationToken = default)
    {
        var q = _db.Reservations
            .Where(x => x.RoomId == roomId && !x.IsCanceled)
            .Where(x => x.StartTime < end && x.EndTime > start);
        if (excludeReservationId.HasValue)
            q = q.Where(x => x.Id != excludeReservationId.Value);
        return await q.ToListAsync(cancellationToken);
    }

    public async Task<Reservation> AddAsync(Reservation reservation, CancellationToken cancellationToken = default)
    {
        _db.Reservations.Add(reservation);
        await _db.SaveChangesAsync(cancellationToken);
        return reservation;
    }

    public async Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken = default)
    {
        _db.Reservations.Update(reservation);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
