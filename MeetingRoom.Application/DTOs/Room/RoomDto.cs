namespace MeetingRoom.Application.DTOs.Room;

public class RoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public int Floor { get; set; }
    public string? Equipment { get; set; }
}
