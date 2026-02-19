namespace MeetingRoom.Domain.Entities;

// Placeholder for recurring series exceptions (skip or delete single occurrence)
public class ReservationException
{
    public int Id { get; set; }
    public int ReservationSeriesId { get; set; }
    public DateTime? ExceptionDate { get; set; }
    public bool IsDeleted { get; set; }
}
