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

        // LocalDB ile çalışanlar için seed; migration ile eklenir
        var now = new DateTime(2025, 2, 1, 0, 0, 0, DateTimeKind.Utc);
        builder.HasData(
            new Reservation { Id = 1, RoomId = 1, UserName = "ali@firma.com", StartTime = now.AddDays(1).AddHours(9), EndTime = now.AddDays(1).AddHours(10), IsCanceled = false, CreatedAt = now },
            new Reservation { Id = 2, RoomId = 1, UserName = "ayse@firma.com", StartTime = now.AddDays(1).AddHours(14), EndTime = now.AddDays(1).AddHours(15), IsCanceled = false, CreatedAt = now },
            new Reservation { Id = 3, RoomId = 2, UserName = "mehmet@firma.com", StartTime = now.AddDays(2).AddHours(10), EndTime = now.AddDays(2).AddHours(11), IsCanceled = false, CreatedAt = now });
    }
}
