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
    }
}
