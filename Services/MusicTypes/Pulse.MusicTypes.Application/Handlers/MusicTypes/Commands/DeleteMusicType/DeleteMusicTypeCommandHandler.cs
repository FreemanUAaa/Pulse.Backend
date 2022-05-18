using MediatR;
using Microsoft.Extensions.Logging;
using Pulse.MusicTypes.Core.Database;
using Pulse.MusicTypes.Core.Exceptions;
using Pulse.MusicTypes.Core.Models;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Commands.DeleteMusicType
{
    public class DeleteMusicTypeCommandHandler : IRequestHandler<DeleteMusicTypeCommand>
    {
        private readonly ILogger<DeleteMusicTypeCommandHandler> logger;

        private readonly IDatabaseContext database;

        public DeleteMusicTypeCommandHandler(IDatabaseContext database, ILogger<DeleteMusicTypeCommandHandler> logger) =>
            (this.database, this.logger) = (database, logger);

        public async Task<Unit> Handle(DeleteMusicTypeCommand request, CancellationToken cancellationToken)
        {
            MusicType? musicType = await database.MusicTypes.FindAsync(new object?[] { request.MusicTypeId }, cancellationToken);

            if (musicType == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            database.MusicTypes.Remove(musicType);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("The music type was be deleted ID: {id}", musicType.Id);

            return Unit.Value;
        }
    }
}
