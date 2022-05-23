using Pulse.Tracks.Core.Models.Base;

namespace Pulse.Tracks.Core.Models
{
    public class TrackLike : BaseEntity
    {
        public Guid TrackId { get; set; }

        public Guid UserId { get; set; }
    }
}
