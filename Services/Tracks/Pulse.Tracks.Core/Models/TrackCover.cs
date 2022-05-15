namespace Pulse.Tracks.Core.Models
{
    public class TrackCover
    {
        public Guid Id { get; set; }

        public Guid TrackId { get; set; }

        public string FileName { get; set; }

        public TrackCover() => FileName = string.Empty;
    }
}
