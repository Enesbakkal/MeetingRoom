namespace MeetingRoom.Domain.Entities;

// Tekrarlayan rezervasyon tanımı (örn. haftalık/aylık). Sadece metadata; seri oluşturmak
// Reservations tablosuna otomatik kayıt eklemez. İleride genişletilebilir.
public class ReservationSeries
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Pattern { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
