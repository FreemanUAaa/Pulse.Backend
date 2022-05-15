namespace Pulse.Tracks.Core.Models
{
    public class Track
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public int Duration { get; set; }

        public string Description { get; set; }

        public Track() => (Name, Description) = (string.Empty, string.Empty);
    }
}
