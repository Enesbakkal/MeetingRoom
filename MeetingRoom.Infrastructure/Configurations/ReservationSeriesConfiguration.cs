using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MeetingRoom.Domain.Entities;

namespace MeetingRoom.Infrastructure.Configurations;

public class ReservationSeriesConfiguration : IEntityTypeConfiguration<ReservationSeries>
{
    public void Configure(EntityTypeBuilder<ReservationSeries> builder)
    {
        builder.ToTable("ReservationSeries");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(200);
        builder.Property(x => x.Pattern).HasMaxLength(50);

        var baseDate = new DateTime(2025, 2, 1, 0, 0, 0, DateTimeKind.Utc);
        builder.HasData(
            new ReservationSeries { Id = 1, Name = "Haftalık Toplantı", Pattern = "Weekly", StartDate = baseDate, EndDate = baseDate.AddMonths(3) },
            new ReservationSeries { Id = 2, Name = "Aylık Değerlendirme", Pattern = "Monthly", StartDate = baseDate.AddDays(7), EndDate = baseDate.AddMonths(6) });
    }
}
