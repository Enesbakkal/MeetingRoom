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
    }
}
