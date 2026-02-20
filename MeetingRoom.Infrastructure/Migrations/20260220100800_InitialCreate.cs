using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeetingRoom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Pattern = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationSeries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    Equipment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservationExceptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationSeriesId = table.Column<int>(type: "int", nullable: false),
                    ExceptionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationExceptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationExceptions_ReservationSeries_ReservationSeriesId",
                        column: x => x.ReservationSeriesId,
                        principalTable: "ReservationSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ReservationSeries",
                columns: new[] { "Id", "EndDate", "Name", "Pattern", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Haftalık Toplantı", "Weekly", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Aylık Değerlendirme", "Monthly", new DateTime(2025, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "Equipment", "Floor", "Name" },
                values: new object[,]
                {
                    { 1, 8, "Projeksiyon, Beyaz Tahta", 1, "Toplantı Odası A" },
                    { 2, 4, "Telefon, TV", 1, "Toplantı Odası B" },
                    { 3, 20, "Projeksiyon, Mikrofon, Beyaz Tahta", 2, "Konferans Salonu" }
                });

            migrationBuilder.InsertData(
                table: "ReservationExceptions",
                columns: new[] { "Id", "ExceptionDate", "IsDeleted", "ReservationSeriesId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, 1 },
                    { 2, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), false, 1 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CreatedAt", "EndTime", "IsCanceled", "RoomId", "StartTime", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 2, 2, 10, 0, 0, 0, DateTimeKind.Utc), false, 1, new DateTime(2025, 2, 2, 9, 0, 0, 0, DateTimeKind.Utc), "ali@firma.com" },
                    { 2, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 2, 2, 15, 0, 0, 0, DateTimeKind.Utc), false, 1, new DateTime(2025, 2, 2, 14, 0, 0, 0, DateTimeKind.Utc), "ayse@firma.com" },
                    { 3, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 2, 3, 11, 0, 0, 0, DateTimeKind.Utc), false, 2, new DateTime(2025, 2, 3, 10, 0, 0, 0, DateTimeKind.Utc), "mehmet@firma.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationExceptions_ReservationSeriesId",
                table: "ReservationExceptions",
                column: "ReservationSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId_StartTime_EndTime",
                table: "Reservations",
                columns: new[] { "RoomId", "StartTime", "EndTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserName",
                table: "Reservations",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Name",
                table: "Rooms",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationExceptions");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "ReservationSeries");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
