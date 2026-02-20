namespace MeetingRoom.Application.DTOs.ReservationSeries;

public class ReservationExceptionDto
{
    public int Id { get; set; }
    public int ReservationSeriesId { get; set; }
    public DateTime? ExceptionDate { get; set; }
    public bool IsDeleted { get; set; }
}
