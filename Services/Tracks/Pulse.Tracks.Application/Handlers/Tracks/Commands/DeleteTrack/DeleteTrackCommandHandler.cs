using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pulse.Tracks.Core.Cache;
using Pulse.Tracks.Core.Exceptions;
using Pulse.Tracks.Core.Helpers.Files;
using Pulse.Tracks.Core.Models;
using Pulse.Tracks.Core.Options;
using Pulse.Tracks.Core.Repositories;

namespace Pulse.Tracks.Application.Handlers.Tracks.Commands.DeleteTrack
{
    public class DeleteTrackCommandHandler : IRequestHandler<DeleteTrackCommand>
    {
        private readonly ILogger<DeleteTrackCommandHandler> logger;

        private readonly IRepositoriesManager repositoriesManager;

        private readonly IDistributedCache cache;

        private readonly PathsOptions paths;

        public DeleteTrackCommandHandler(IRepositoriesManager repositoriesManager, IDistributedCache cache, IOptions<PathsOptions> paths, ILogger<DeleteTrackCommandHandler> logger) =>
            (this.repositoriesManager, this.cache, this.paths, this.logger) = (repositoriesManager, cache, paths.Value, logger);

        public async Task<Unit> Handle(DeleteTrackCommand request, CancellationToken cancellationToken)
        {
            Track? track = await repositoriesManager.Track.Get(x => x.Id == request.TrackId && x.UserId == request.UserId);
        
            if (track == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            TrackCover cover = (await repositoriesManager.TrackCover.Get(x => x.TrackId == track.Id))!;
            TrackSong song = (await repositoriesManager.TrackSong.Get(x => x.TrackId == track.Id))!;

            string coverFilePath = Path.Combine(paths.TrackCovers, cover.FileName);
            string songFilePath = Path.Combine(paths.TrackSongs, song.FileName);

            try
            {
                await FileManager.DeleteFile(coverFilePath);
                await FileManager.DeleteFile(songFilePath);
            }
            catch
            {
                throw new Exception(ExceptionStrings.FailedDeleteFile);
            }

            await repositoriesManager.TrackCover.Delete(cover);
            await repositoriesManager.TrackSong.Delete(song);
            await repositoriesManager.Track.Delete(track);

            await cache.RemoveAsync(CacheContracts.GetTrackDetailsKey(request.TrackId), cancellationToken);

            await repositoriesManager.Save();

            logger.LogInformation("The track was be deleted ID: {id}", track.Id);

            return Unit.Value;
        }
    }
}
