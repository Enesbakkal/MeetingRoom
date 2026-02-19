using Microsoft.EntityFrameworkCore;
using MeetingRoom.Application.Interfaces;
using MeetingRoom.Domain.Entities;
using MeetingRoom.Infrastructure.Data;

namespace MeetingRoom.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _db;

    public RoomRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Room?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _db.Rooms.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IReadOnlyList<Room>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _db.Rooms.ToListAsync(cancellationToken);
    }

    public async Task<Room> AddAsync(Room room, CancellationToken cancellationToken = default)
    {
        _db.Rooms.Add(room);
        await _db.SaveChangesAsync(cancellationToken);
        return room;
    }

    public async Task UpdateAsync(Room room, CancellationToken cancellationToken = default)
    {
        _db.Rooms.Update(room);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var room = await _db.Rooms.FindAsync(new object[] { id }, cancellationToken);
        if (room != null)
        {
            _db.Rooms.Remove(room);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
