using MediatR;
using Pulse.Tracks.Core.Exceptions;
using Pulse.Tracks.Core.Models;
using Pulse.Tracks.Core.Repositories;

namespace Pulse.Tracks.Application.Handlers.Tracks.Queries.GetTrackDetails
{
    public class GetTrackDetailsQueryHandler : IRequestHandler<GetTrackDetailsQuery, Track>
    {
        private readonly IRepositoriesManager repositoriesManager;

        public GetTrackDetailsQueryHandler(IRepositoriesManager repositoriesManager) => this.repositoriesManager = repositoriesManager;

        public async Task<Track> Handle(GetTrackDetailsQuery request, CancellationToken cancellationToken)
        {
            Track? track = await repositoriesManager.Track.Get(x => x.Id == request.TrackId);

            if (track == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            return track;
        }
    }
}
