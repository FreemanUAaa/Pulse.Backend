using Pulse.Tracks.Core.Models;
using System.Linq.Expressions;

namespace Pulse.Tracks.Core.Repositories
{
    public interface ITrackRepository
    {
        Task<Track?> Get(Guid trackId);

        Task<IEnumerable<Track?>> GetList(Expression<Func<Track, bool>> predicate);

        Task Add(Track track);

        Task Update(Track track);

        Task Delete(Track track);

        Task Delete(Guid trackId);

        Task Save();
    }
}
