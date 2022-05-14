using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pulse.Users.Core.Database;
using Pulse.Users.Core.Exceptions;
using Pulse.Users.Core.Models;
using Pulse.Users.Core.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Pulse.Users.Application.Handlers.Covers.Queries.GetCoverBytes
{
    public class GetCoverBytesQueryHandler : IRequestHandler<GetCoverBytesQuery, GetCoverBytesVm>
    {
        private readonly ILogger<GetCoverBytesQueryHandler> logger;

        private readonly IDatabaseContext database;

        private readonly PathOptions path;

        public GetCoverBytesQueryHandler(IDatabaseContext database, IOptions<PathOptions> path, ILogger<GetCoverBytesQueryHandler> logger) =>
            (this.database, this.path, this.logger) = (database, path.Value, logger);

        public async Task<GetCoverBytesVm> Handle(GetCoverBytesQuery request, CancellationToken cancellationToken)
        {
            Cover cover = await database.Covers.FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

            if (cover == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            string filePath = Path.Combine(path.Cover, cover.FileName);

            byte[] bytes = await File.ReadAllBytesAsync(filePath, cancellationToken);

            string extension = Path.GetExtension(cover.FileName);

            logger.LogInformation("The cover was received");

            return new GetCoverBytesVm() { Bytes = bytes, Extension = extension };
        }
    }
}
