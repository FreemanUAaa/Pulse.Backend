using Pulse.Tracks.Core.DTOs;
using Pulse.Tracks.Core.Models;
using System.Linq.Expressions;

namespace Pulse.Tracks.Core.Repositories
{
    public interface IMusicTypeRepository
    {
        Task<IEnumerable<MusicTypeIsExistsDTO>> IsExists(IEnumerable<Guid> musictTypeIds);

        Task<IEnumerable<MusicType?>> GetList(Expression<Func<MusicType, bool>> predicate);

        Task Add(MusicType musicType);

        Task Update(MusicType musicType);

        Task Delete(MusicType musicType);

        Task Delete(Guid musicTypeId);

        Task Save();
    }
}
