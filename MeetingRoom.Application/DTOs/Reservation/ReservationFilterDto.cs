namespace MeetingRoom.Application.DTOs.Reservation;

public class ReservationFilterDto
{
    public int? RoomId { get; set; }
    public string? UserName { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}
