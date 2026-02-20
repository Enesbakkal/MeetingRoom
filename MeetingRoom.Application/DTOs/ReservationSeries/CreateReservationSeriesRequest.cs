namespace MeetingRoom.Application.DTOs.ReservationSeries;

public class CreateReservationSeriesRequest
{
    public string? Name { get; set; }
    public string? Pattern { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
