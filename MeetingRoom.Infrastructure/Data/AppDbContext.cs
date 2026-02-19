using Microsoft.EntityFrameworkCore;
using MeetingRoom.Domain.Entities;

namespace MeetingRoom.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<ReservationSeries> ReservationSeries => Set<ReservationSeries>();
    public DbSet<ReservationException> ReservationExceptions => Set<ReservationException>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
