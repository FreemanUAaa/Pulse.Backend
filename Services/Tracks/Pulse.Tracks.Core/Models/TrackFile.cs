namespace Pulse.Tracks.Core.Models
{
    public class TrackFile
    {
        public Guid Id { get; set; }

        public Guid TrackId { get; set; }

        public string FileName { get; set; }

        public TrackFile() => FileName = string.Empty;
    }
}
