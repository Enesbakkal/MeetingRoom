namespace MeetingRoom.Domain.Entities;

// Pure domain entity; no EF attributes
public class Reservation
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string UserName { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsCanceled { get; set; }
    public DateTime CreatedAt { get; set; }
}
