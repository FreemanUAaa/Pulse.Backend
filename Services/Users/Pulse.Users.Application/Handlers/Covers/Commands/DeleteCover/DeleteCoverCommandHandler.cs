using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pulse.Users.Core.Database;
using Pulse.Users.Core.Exceptions;
using Pulse.Users.Core.Models;
using Pulse.Users.Core.Options;
using Pulse.Users.Core.Services;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Pulse.Users.Application.Handlers.Covers.Commands.DeleteCover
{
    public class DeleteCoverCommandHandler : IRequestHandler<DeleteCoverCommand>
    {
        private readonly ILogger<DeleteCoverCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IFileManager fileManager;

        private readonly PathOptions path;

        public DeleteCoverCommandHandler(IDatabaseContext database, IFileManager fileManager, IOptions<PathOptions> path, ILogger<DeleteCoverCommandHandler> logger) =>
            (this.database, this.fileManager, this.path, this.logger) = (database, fileManager, path.Value, logger);

        public async Task<Unit> Handle(DeleteCoverCommand request, CancellationToken cancellationToken)
        {
            Cover cover = await database.Covers.FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

            if (cover == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            string filePath = Path.Combine(path.Cover, cover.FileName);

            if (File.Exists(filePath))
            {
                try
                {
                    await fileManager.DeleteFile(filePath);
                }
                catch
                {
                    throw new Exception(ExceptionStrings.FailedDeleteFile);
                }
            }

            cover.FileName = string.Empty;

            database.Covers.Update(cover);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("The cover was deleted ID: {Id}", cover.Id);

            return Unit.Value;
        }
    }
}
