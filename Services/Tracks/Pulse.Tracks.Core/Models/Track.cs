using Pulse.Tracks.Core.Models.Base;

namespace Pulse.Tracks.Core.Models
{
    public class Track : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public int Duration { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
