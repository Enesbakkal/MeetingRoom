using Microsoft.EntityFrameworkCore;
using MeetingRoom.Api.Entities;

namespace MeetingRoom.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Reservation> Reservations => Set<Reservation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservation>()
                .HasIndex(r => new { r.RoomId, r.StartTime, r.EndTime });
        }
    }
}
