using MeetingRoom.Application.DTOs.Room;

namespace MeetingRoom.Application.Services;

public interface IRoomService
{
    Task<RoomDto> CreateAsync(CreateRoomDto dto, CancellationToken cancellationToken = default);
    Task<RoomDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RoomDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RoomDto?> UpdateAsync(int id, UpdateRoomDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
