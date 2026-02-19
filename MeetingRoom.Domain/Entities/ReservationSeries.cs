namespace MeetingRoom.Domain.Entities;

// Placeholder for future recurring meetings (e.g. Daily, Weekly)
public class ReservationSeries
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Pattern { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
