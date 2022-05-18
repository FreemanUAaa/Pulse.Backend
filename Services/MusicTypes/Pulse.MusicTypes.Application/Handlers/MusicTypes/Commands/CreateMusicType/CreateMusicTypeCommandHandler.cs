using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pulse.MusicTypes.Core.Database;
using Pulse.MusicTypes.Core.Exceptions;
using Pulse.MusicTypes.Core.Models;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Commands.CreateMusicType
{
    public class CreateMusicTypeCommandHandler : IRequestHandler<CreateMusicTypeCommand, Guid>
    {
        private readonly ILogger<CreateMusicTypeCommandHandler> logger;

        private readonly IDatabaseContext database;

        public CreateMusicTypeCommandHandler(IDatabaseContext database, ILogger<CreateMusicTypeCommandHandler> logger) =>
            (this.database, this.logger) = (database, logger);

        public async Task<Guid> Handle(CreateMusicTypeCommand request, CancellationToken cancellationToken)
        {
            bool nameIsUsed = await database.MusicTypes.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);

            if (nameIsUsed)
            {
                throw new Exception(ExceptionStrings.EntityAlreadyExists);
            }

            MusicType musicType = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
            };

            database.MusicTypes.Add(musicType);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("The music type was be created ID: {id}", musicType.Id);

            return musicType.Id;
        }
    }
}
