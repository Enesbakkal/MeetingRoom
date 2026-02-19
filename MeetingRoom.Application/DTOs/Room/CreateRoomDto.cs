namespace MeetingRoom.Application.DTOs.Room;

public class CreateRoomDto
{
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public int Floor { get; set; }
    public string? Equipment { get; set; }
}
