using MediatR;
using Microsoft.Extensions.Logging;
using Pulse.MusicTypes.Core.Database;
using Pulse.MusicTypes.Core.Exceptions;
using Pulse.MusicTypes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Commands.UpdateMusicType
{
    public class UpdateMusicTypeCommandHandler : IRequestHandler<UpdateMusicTypeCommand>
    {
        private readonly ILogger<UpdateMusicTypeCommandHandler> logger;

        private readonly IDatabaseContext database;

        public UpdateMusicTypeCommandHandler(IDatabaseContext database, ILogger<UpdateMusicTypeCommandHandler> logger) =>
            (this.database, this.logger) = (database, logger);

        public async Task<Unit> Handle(UpdateMusicTypeCommand request, CancellationToken cancellationToken)
        {
            MusicType? musicType = await database.MusicTypes.FindAsync(new object[] { request.MusicTypeId }, cancellationToken);

            if (musicType == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            musicType.Name = request.Name;

            database.MusicTypes.Update(musicType);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("The music type was be updated ID: {id}", musicType.Id);

            return Unit.Value;
        }
    }
}
