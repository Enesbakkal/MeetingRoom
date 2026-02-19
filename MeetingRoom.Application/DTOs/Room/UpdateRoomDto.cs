namespace MeetingRoom.Application.DTOs.Room;

public class UpdateRoomDto
{
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public int Floor { get; set; }
    public string? Equipment { get; set; }
}
