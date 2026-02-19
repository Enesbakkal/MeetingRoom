using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MeetingRoom.Domain.Entities;

namespace MeetingRoom.Infrastructure.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.RoomId).IsRequired();
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.StartTime).IsRequired();
        builder.Property(x => x.EndTime).IsRequired();
        builder.Property(x => x.IsCanceled).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasOne<Room>().WithMany().HasForeignKey(x => x.RoomId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.RoomId);
        builder.HasIndex(x => x.UserName);
        builder.HasIndex(x => new { x.RoomId, x.StartTime, x.EndTime });
    }
}
