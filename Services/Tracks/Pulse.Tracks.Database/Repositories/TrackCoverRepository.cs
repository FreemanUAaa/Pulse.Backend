using Pulse.Tracks.Core.Database;
using Pulse.Tracks.Core.Models;
using Pulse.Tracks.Core.Repositories;
using Pulse.Tracks.Database.Repositories.Base;

namespace Pulse.Tracks.Database.Repositories
{
    public class TrackCoverRepository : BaseRepository<TrackCover>, ITrackCoverRepository
    {
        public TrackCoverRepository(IDatabaseContext database) : base(database) { }
    }
}
