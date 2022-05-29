using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pulse.Tracks.Core.DTOs;
using Pulse.Tracks.Core.Exceptions;
using Pulse.Tracks.Core.Helpers.Extensions;
using Pulse.Tracks.Core.Helpers.Files;
using Pulse.Tracks.Core.Models;
using Pulse.Tracks.Core.Options;
using Pulse.Tracks.Core.Repositories;

namespace Pulse.Tracks.Application.Handlers.Tracks.Commands.CreateTrack
{
    public class CreateTrackCommandHandler : IRequestHandler<CreateTrackCommand, Guid>
    {
        private readonly ILogger<CreateTrackCommandHandler> logger;

        private readonly IRepositoriesManager repositoriesManager;

        private readonly PathsOptions paths;

        public CreateTrackCommandHandler(IRepositoriesManager repositoriesManager, IOptions<PathsOptions> paths, ILogger<CreateTrackCommandHandler> logger) =>
            (this.repositoriesManager, this.paths, this.logger) = (repositoriesManager, paths.Value, logger);

        public async Task<Guid> Handle(CreateTrackCommand request, CancellationToken cancellationToken)
        {
            if (! await repositoriesManager.User.IsExists(request.UserId))
            {
                throw new Exception(ExceptionStrings.UserNotFound);
            }

            List<MusicTypeIsExistsDTO> isExists = (await repositoriesManager.MusicType.IsExists(request.MusicTypeIds)).ToList();

            if (isExists.Any(x => x.IsExists == false))
            {
                throw new Exception(ExceptionStrings.MusicTypeNotFound);
            }

            Track track = new()
            {
                Listening = 0,
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedDate = DateTime.Now,
                UserId = request.UserId,
                Duration = request.Duration,
                Description = request.Description,
            };

            string coverFileName = await FileManager.SaveFile(request.Cover, paths.TrackCovers, x => ExtensionValidator.ValidateCoverExtension(x));

            string songFileName = await FileManager.SaveFile(request.Song, paths.TrackSongs, x => ExtensionValidator.ValidateSongExtension(x));

            TrackCover cover = new()
            {
                FileName = coverFileName,
                TrackId = track.Id,
                Id = Guid.NewGuid(),
            };

            TrackSong song = new()
            {
                FileName = songFileName,
                TrackId = track.Id,
                Id = Guid.NewGuid(),
            };

            foreach (Guid musicTypeId in request.MusicTypeIds)
            {
                MusicType musicType = new()
                {
                    Id = Guid.NewGuid(),
                    TrackId = track.Id,
                    MusicTypeId = musicTypeId,
                };

                await repositoriesManager.MusicType.Add(musicType);
            }

            await repositoriesManager.TrackCover.Add(cover);
            await repositoriesManager.TrackSong.Add(song);

            await repositoriesManager.Track.Add(track);

            await repositoriesManager.Save();

            logger.LogInformation("The track was be created ID: {id}", track.Id);

            return track.Id;
        }
    }
}
