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
    }
}
