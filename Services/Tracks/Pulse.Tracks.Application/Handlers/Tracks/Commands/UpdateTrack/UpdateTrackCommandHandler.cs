using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pulse.Tracks.Core.Cache;
using Pulse.Tracks.Core.DTOs;
using Pulse.Tracks.Core.Exceptions;
using Pulse.Tracks.Core.Helpers.Extensions;
using Pulse.Tracks.Core.Helpers.Files;
using Pulse.Tracks.Core.Models;
using Pulse.Tracks.Core.Options;
using Pulse.Tracks.Core.Repositories;

namespace Pulse.Tracks.Application.Handlers.Tracks.Commands.UpdateTrack
{
    public class UpdateTrackCommandHandler : IRequestHandler<UpdateTrackCommand>
    {
        private readonly ILogger<UpdateTrackCommandHandler> logger;

        private readonly IRepositoriesManager repositoriesManager;

        private readonly IDistributedCache cache;

        private readonly PathsOptions paths;

        public UpdateTrackCommandHandler(IRepositoriesManager repositoriesManager, IDistributedCache cache, IOptions<PathsOptions> paths, ILogger<UpdateTrackCommandHandler> logger) =>
            (this.repositoriesManager, this.cache, this.paths, this.logger) = (repositoriesManager, cache, paths.Value, logger);

        public async Task<Unit> Handle(UpdateTrackCommand request, CancellationToken cancellationToken)
        {
            Track? track = await repositoriesManager.Track.Get(x => x.Id == request.TrackId && x.UserId == request.UserId);

            if (track == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            (track.Name, track.Description, track.Duration) = (request.Name, request.Description, track.Duration);
        
            if (request.MusicTypeIds != null)
            {
                List<MusicTypeIsExistsDTO> isExists = (await repositoriesManager.MusicType.IsExists(request.MusicTypeIds)).ToList();
                
                if (isExists.Any(x => x.IsExists == false))
                {
                    throw new Exception(ExceptionStrings.MusicTypeNotFound);
                }

                await UpdateMusicTypes(track.Id, request.MusicTypeIds);
            }

            if (request.Cover != null)
            {
                TrackCover cover = (await repositoriesManager.TrackCover.Get(x => x.TrackId == track.Id))!;

                cover.FileName = await UpdateFile(request.Cover, paths.TrackCovers, cover.FileName, x => ExtensionValidator.ValidateCoverExtension(x));

                await repositoriesManager.TrackCover.Update(cover);
            }

            if (request.Song != null)
            {
                TrackSong song = (await repositoriesManager.TrackSong.Get(x => x.TrackId == track.Id))!;

                song.FileName = await UpdateFile(request.Song, paths.TrackCovers, song.FileName, x => ExtensionValidator.ValidateSongExtension(x));

                await repositoriesManager.TrackSong.Update(song);
            }

            await cache.RemoveAsync(CacheContracts.GetTrackDetailsKey(request.TrackId), cancellationToken);

            await repositoriesManager.Track.Update(track);
            await repositoriesManager.Save();

            logger.LogInformation("The track was be updated ID: {id}", track.Id);

            return Unit.Value;
        }

        private async Task UpdateMusicTypes(Guid trackId, IEnumerable<Guid> newMusicTypeIds)
        {
            List<MusicType?> musicTypes = (await repositoriesManager.MusicType.GetList(x => x.TrackId == trackId)).ToList();

            foreach (MusicType? musicType in musicTypes)
            {
                if (musicType != null)
                {
                    await repositoriesManager.MusicType.Delete(musicType);
                }
            }

            foreach (Guid musicTypeId in newMusicTypeIds)
            {
                MusicType musicType = new()
                {
                    Id = Guid.NewGuid(),
                    TrackId = trackId,
                    MusicTypeId = musicTypeId,
                };

                await repositoriesManager.MusicType.Add(musicType);
            }
        }

        private static async Task<string> UpdateFile(IFormFile file, string path, string oldFileName, Func<string, bool> extensionValidator)
        {
            string oldFilePath = Path.Combine(path, oldFileName);

            string extension = Path.GetExtension(file.FileName);

            string newFileName;

            try
            {
                newFileName = FileNameGenerator.GenerateUniqueFileName(path, extension, 10);
                string newFilePath = Path.Combine(path, newFileName);

                await FileManager.DeleteFile(path);
                await FileManager.SaveFile(file, newFilePath, extensionValidator);
            }
            catch
            {
                throw new Exception(ExceptionStrings.FailedSaveFile);
            }

            return newFileName;
        }
    }
}
