using Pulse.Tracks.Core.Models;
using System.Linq.Expressions;

namespace Pulse.Tracks.Core.Repositories
{
    public interface ITrackCoverRepository
    {
        Task<TrackCover?> Get(Expression<Func<TrackCover, bool>> predicate);

        Task Add(TrackCover cover);

        Task Update(TrackCover cover);

        Task Delete(TrackCover cover);

        Task Delete(Guid coverId);

        Task Save();
    }
}
