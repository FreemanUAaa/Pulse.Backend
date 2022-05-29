using MediatR;
using Microsoft.Extensions.Options;
using Pulse.Tracks.Core.Exceptions;
using Pulse.Tracks.Core.Models;
using Pulse.Tracks.Core.Options;
using Pulse.Tracks.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulse.Tracks.Application.Handlers.Covers.Queries.GetCoverBytes
{
    public class GetCoverBytesQueryHandler : IRequestHandler<GetCoverBytesQuery, GetCoverBytesQueryVm>
    {
        private readonly IRepositoriesManager repositoriesManager;

        private readonly PathsOptions paths;

        public GetCoverBytesQueryHandler(IRepositoriesManager repositoriesManager, IOptions<PathsOptions> paths) =>
            (this.repositoriesManager, this.paths) = (repositoriesManager, paths.Value);

        public async Task<GetCoverBytesQueryVm> Handle(GetCoverBytesQuery request, CancellationToken cancellationToken)
        {
            TrackCover? cover = await repositoriesManager.TrackCover.Get(x => x.TrackId == request.TrackId);

            if (cover == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            string coverFilePath = Path.Combine(paths.TrackCovers, cover.FileName);

            if (!File.Exists(coverFilePath))
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            byte[] bytes = Array.Empty<byte>();

            try
            {
                bytes = await File.ReadAllBytesAsync(coverFilePath, cancellationToken);
            }
            catch
            {
                throw new Exception(ExceptionStrings.ErrorOccurred);
            }

            string extension = Path.GetExtension(cover.FileName);

            return new() { Bytes = bytes, Extension = extension };
        }
    }
}
