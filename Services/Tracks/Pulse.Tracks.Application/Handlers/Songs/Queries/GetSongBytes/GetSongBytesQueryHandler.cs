using MediatR;
using Microsoft.Extensions.Options;
using Pulse.Tracks.Core.Exceptions;
using Pulse.Tracks.Core.Models;
using Pulse.Tracks.Core.Options;
using Pulse.Tracks.Core.Repositories;

namespace Pulse.Tracks.Application.Handlers.Songs.Queries.GetSongBytes
{
    public class GetSongBytesQueryHandler : IRequestHandler<GetSongBytesQuery, GetSongBytesQueryVm>
    {
        private readonly IRepositoriesManager repositoriesManager;

        private readonly PathsOptions paths;

        public GetSongBytesQueryHandler(IOptions<PathsOptions> paths, IRepositoriesManager repositoriesManager) =>
            (this.repositoriesManager, this.paths) = (repositoriesManager, paths.Value);

        public async Task<GetSongBytesQueryVm> Handle(GetSongBytesQuery request, CancellationToken cancellationToken)
        {
            TrackSong? song = await repositoriesManager.TrackSong.Get(x => x.TrackId == request.TrackId);

            if (song == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            string songFilePath = Path.Combine(paths.TrackSongs, song.FileName);

            if (!File.Exists(songFilePath))
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            byte[] bytes = Array.Empty<byte>();

            try
            {
                bytes = await File.ReadAllBytesAsync(songFilePath);
            }
            catch
            {
                throw new Exception(ExceptionStrings.ErrorOccurred);
            }

            string extension = Path.GetExtension(song.FileName);

            return new() { Bytes = bytes, Extension = extension };
        }
    }
}
