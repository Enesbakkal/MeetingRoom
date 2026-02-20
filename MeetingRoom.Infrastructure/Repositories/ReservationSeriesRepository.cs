using Microsoft.EntityFrameworkCore;
using MeetingRoom.Application.Interfaces;
using MeetingRoom.Domain.Entities;
using MeetingRoom.Infrastructure.Data;

namespace MeetingRoom.Infrastructure.Repositories;

public class ReservationSeriesRepository : IReservationSeriesRepository
{
    private readonly AppDbContext _db;

    public ReservationSeriesRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ReservationSeries?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _db.ReservationSeries.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IReadOnlyList<ReservationSeries>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _db.ReservationSeries.ToListAsync(cancellationToken);
    }

    public async Task<ReservationSeries> AddAsync(ReservationSeries series, CancellationToken cancellationToken = default)
    {
        _db.ReservationSeries.Add(series);
        await _db.SaveChangesAsync(cancellationToken);
        return series;
    }

    public async Task UpdateAsync(ReservationSeries series, CancellationToken cancellationToken = default)
    {
        _db.ReservationSeries.Update(series);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var series = await _db.ReservationSeries.FindAsync(new object[] { id }, cancellationToken);
        if (series != null)
        {
            _db.ReservationSeries.Remove(series);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
