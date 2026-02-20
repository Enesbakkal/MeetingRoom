using Microsoft.EntityFrameworkCore;
using MeetingRoom.Application.Interfaces;
using MeetingRoom.Domain.Entities;
using MeetingRoom.Infrastructure.Data;

namespace MeetingRoom.Infrastructure.Repositories;

public class ReservationExceptionRepository : IReservationExceptionRepository
{
    private readonly AppDbContext _db;

    public ReservationExceptionRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ReservationException?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _db.ReservationExceptions.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IReadOnlyList<ReservationException>> GetBySeriesIdAsync(int seriesId, CancellationToken cancellationToken = default)
    {
        return await _db.ReservationExceptions
            .Where(x => x.ReservationSeriesId == seriesId)
            .OrderBy(x => x.ExceptionDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<ReservationException> AddAsync(ReservationException exception, CancellationToken cancellationToken = default)
    {
        _db.ReservationExceptions.Add(exception);
        await _db.SaveChangesAsync(cancellationToken);
        return exception;
    }

    public async Task UpdateAsync(ReservationException exception, CancellationToken cancellationToken = default)
    {
        _db.ReservationExceptions.Update(exception);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
