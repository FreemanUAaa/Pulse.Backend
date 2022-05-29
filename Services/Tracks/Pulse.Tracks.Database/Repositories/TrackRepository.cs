using Microsoft.EntityFrameworkCore;
using Pulse.Tracks.Core.Database;
using Pulse.Tracks.Core.Models;
using Pulse.Tracks.Core.Repositories;
using Pulse.Tracks.Database.Repositories.Base;
using System.Linq.Expressions;

namespace Pulse.Tracks.Database.Repositories
{
    public class TrackRepository : BaseRepository<Track>, ITrackRepository
    {
        public TrackRepository(IDatabaseContext database) : base(database) { }
    }
}
