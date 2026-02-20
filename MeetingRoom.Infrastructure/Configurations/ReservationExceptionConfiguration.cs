using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MeetingRoom.Domain.Entities;

namespace MeetingRoom.Infrastructure.Configurations;

public class ReservationExceptionConfiguration : IEntityTypeConfiguration<ReservationException>
{
    public void Configure(EntityTypeBuilder<ReservationException> builder)
    {
        builder.ToTable("ReservationExceptions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ReservationSeriesId).IsRequired();
        builder.HasIndex(x => x.ReservationSeriesId);

        builder.HasOne<ReservationSeries>().WithMany().HasForeignKey(x => x.ReservationSeriesId).OnDelete(DeleteBehavior.Restrict);

        var baseDate = new DateTime(2025, 2, 1, 0, 0, 0, DateTimeKind.Utc);
        builder.HasData(
            new ReservationException { Id = 1, ReservationSeriesId = 1, ExceptionDate = baseDate.AddDays(7), IsDeleted = false },
            new ReservationException { Id = 2, ReservationSeriesId = 1, ExceptionDate = baseDate.AddDays(14), IsDeleted = false });
    }
}
