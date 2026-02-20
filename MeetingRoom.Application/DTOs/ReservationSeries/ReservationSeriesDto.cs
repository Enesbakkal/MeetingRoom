namespace MeetingRoom.Application.DTOs.ReservationSeries;

public class ReservationSeriesDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Pattern { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
