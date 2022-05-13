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
using Pulse.Users.Core.Helpers.Files;
using Pulse.Users.Core.Helpers.Extensions;

namespace Pulse.Users.Application.Handlers.Covers.Commands.UpdateCover
{
    public class UpdateCoverCommandHandler : IRequestHandler<UpdateCoverCommand>
    {
        private readonly ILogger<UpdateCoverCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IFileManager fileManager;

        private readonly PathOptions path;

        public UpdateCoverCommandHandler(IDatabaseContext database, IFileManager fileManager, IOptions<PathOptions> path, ILogger<UpdateCoverCommandHandler> logger) =>
            (this.database, this.fileManager, this.path, this.logger) = (database, fileManager, path.Value, logger);

        public async Task<Unit> Handle(UpdateCoverCommand request, CancellationToken cancellationToken)
        {
            Cover cover = await database.Covers.FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

            if (cover == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            string fileExtension = Path.GetExtension(request.File.FileName);

            if (!ExtensionChaker.IsCorrectCoverExtension(fileExtension))
            {
                throw new Exception(ExceptionStrings.ExtensionNotSupported);
            }

            string newFileName = FileNameGenerator.GenerateUniqueFileName(path.Cover, fileExtension, 10);

            if (newFileName == null)
            {
                throw new Exception(ExceptionStrings.FailedCreateFile);
            }

            if (!string.IsNullOrEmpty(cover.FileName))
            {
                string filePath = Path.Combine(path.Cover, cover.FileName);

                try
                {
                    await fileManager.DeleteFile(filePath);
                }
                catch
                {
                    throw new Exception(ExceptionStrings.FailedCreateFile);
                }
            }
            try
            {
                string newFilePath = Path.Combine(path.Cover, newFileName);
                await fileManager.SaveFile(request.File, newFilePath);
            }

            catch
            {
                throw new Exception(ExceptionStrings.FailedCreateFile);
            }

            cover.FileName = newFileName;

            database.Covers.Update(cover);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("The coverwas updated ID: {Id}", cover.Id);

            return Unit.Value;
        }
    }
}
