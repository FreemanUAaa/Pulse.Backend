using MediatR;
using Pulse.Tracks.Core.Cache;
using Pulse.Tracks.Core.Interfaces.Caching;
using Pulse.Tracks.Core.Models;

namespace Pulse.Tracks.Application.Handlers.Tracks.Queries.GetTrackDetails
{
    public class GetTrackDetailsQuery : IRequest<Track>, ICacheableMediatorQuery
    {
        public Guid TrackId { get; set; }

        public string CacheKey => CacheContracts.GetTrackDetailsKey(TrackId);
    }
}
