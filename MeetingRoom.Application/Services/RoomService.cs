using MeetingRoom.Application.DTOs.Room;
using MeetingRoom.Application.Interfaces;
using MeetingRoom.Domain.Entities;

namespace MeetingRoom.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repository;

    public RoomService(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task<RoomDto> CreateAsync(CreateRoomDto dto, CancellationToken cancellationToken = default)
    {
        var room = new Room
        {
            Name = dto.Name,
            Capacity = dto.Capacity,
            Floor = dto.Floor,
            Equipment = dto.Equipment
        };
        var created = await _repository.AddAsync(room, cancellationToken);
        return MapToDto(created);
    }

    public async Task<RoomDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var room = await _repository.GetByIdAsync(id, cancellationToken);
        return room is null ? null : MapToDto(room);
    }

    public async Task<IReadOnlyList<RoomDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var rooms = await _repository.GetAllAsync(cancellationToken);
        return rooms.Select(MapToDto).ToList();
    }

    public async Task<RoomDto?> UpdateAsync(int id, UpdateRoomDto dto, CancellationToken cancellationToken = default)
    {
        var room = await _repository.GetByIdAsync(id, cancellationToken);
        if (room is null) return null;
        room.Name = dto.Name;
        room.Capacity = dto.Capacity;
        room.Floor = dto.Floor;
        room.Equipment = dto.Equipment;
        await _repository.UpdateAsync(room, cancellationToken);
        return MapToDto(room);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var room = await _repository.GetByIdAsync(id, cancellationToken);
        if (room is null) return false;
        await _repository.DeleteAsync(id, cancellationToken);
        return true;
    }

    private static RoomDto MapToDto(Room room)
    {
        return new RoomDto
        {
            Id = room.Id,
            Name = room.Name,
            Capacity = room.Capacity,
            Floor = room.Floor,
            Equipment = room.Equipment
        };
    }
}
