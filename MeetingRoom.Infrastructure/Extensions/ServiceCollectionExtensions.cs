using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using MeetingRoom.Application.Behaviors;
using MeetingRoom.Application.Interfaces;
using MeetingRoom.Application.Services;
using MeetingRoom.Application.Validators;
using MeetingRoom.Infrastructure.Data;
using MeetingRoom.Infrastructure.Repositories;

namespace MeetingRoom.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Varsayılan: appsettings.json'daki LocalDB. Kendi SQL için aşağıyı açıp connectionString atayın; repoya göndermeyin.
        // var connectionString = "Server=.;Database=MeetingRoom;Integrated Security=True;TrustServerCertificate=True;";
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is not set.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IReservationSeriesRepository, ReservationSeriesRepository>();
        services.AddScoped<IReservationExceptionRepository, ReservationExceptionRepository>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddValidatorsFromAssemblyContaining<CreateRoomDtoValidator>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IRoomService).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
