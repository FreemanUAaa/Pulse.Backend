using Pulse.Tracks.Core.Models;
using System.Linq.Expressions;

namespace Pulse.Tracks.Core.Repositories
{
    public interface ITrackSongRepository
    {
        Task<TrackSong?> Get(Expression<Func<TrackSong, bool>> predicate);

        Task Add(TrackSong song);

        Task Update(TrackSong song);

        Task Delete(TrackSong song);

        Task Delete(Guid songId);

        Task Save();
    }
}
