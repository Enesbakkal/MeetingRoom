using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MeetingRoom.Domain.Entities;

namespace MeetingRoom.Infrastructure.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Capacity).IsRequired();
        builder.Property(x => x.Floor).IsRequired();
        builder.Property(x => x.Equipment).HasMaxLength(500);
        builder.HasIndex(x => x.Name);

        // LocalDB ile çalışanlar için seed; migration ile eklenir
        builder.HasData(
            new Room { Id = 1, Name = "Toplantı Odası A", Capacity = 8, Floor = 1, Equipment = "Projeksiyon, Beyaz Tahta" },
            new Room { Id = 2, Name = "Toplantı Odası B", Capacity = 4, Floor = 1, Equipment = "Telefon, TV" },
            new Room { Id = 3, Name = "Konferans Salonu", Capacity = 20, Floor = 2, Equipment = "Projeksiyon, Mikrofon, Beyaz Tahta" });
    }
}
