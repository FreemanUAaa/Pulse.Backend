using Pulse.Tracks.Core.Models.Base;

namespace Pulse.Tracks.Core.Models
{
    public class TrackCover : BaseEntity
    {
        public Guid TrackId { get; set; }

        public string FileName { get; set; } = string.Empty;
    }
}
