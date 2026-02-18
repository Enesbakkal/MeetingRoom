namespace MeetingRoom.Api.Entities
{
    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public int Floor { get; set; }

        // Simple string for equipment list for now
        public string? Equipment { get; set; }
    }
}
