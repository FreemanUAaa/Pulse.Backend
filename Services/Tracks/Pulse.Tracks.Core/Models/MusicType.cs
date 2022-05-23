using Pulse.Tracks.Core.Models.Base;

namespace Pulse.Tracks.Core.Models
{
    public class MusicType : BaseEntity
    {
        public Guid TrackId { get; set; }

        public Guid MusicTypeId { get; set; }
    }
}
