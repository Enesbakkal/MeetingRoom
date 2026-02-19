namespace MeetingRoom.Application.DTOs.Reservation;

public class CreateReservationDto
{
    public int RoomId { get; set; }
    public string UserName { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
