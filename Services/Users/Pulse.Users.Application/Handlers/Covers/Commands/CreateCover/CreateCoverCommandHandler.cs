using MediatR;
using Microsoft.Extensions.Logging;
using Pulse.Users.Core.Database;
using Pulse.Users.Core.Exceptions;
using Pulse.Users.Core.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pulse.Users.Application.Handlers.Covers.Commands.CreateCover
{
    public class CreateCoverCommandHandler : IRequestHandler<CreateCoverCommand, Guid>
    {
        private readonly ILogger<CreateCoverCommandHandler> logger;

        private readonly IDatabaseContext database;

        public CreateCoverCommandHandler(IDatabaseContext database, ILogger<CreateCoverCommandHandler> logger) =>
            (this.database, this.logger) = (database, logger);

        public async Task<Guid> Handle(CreateCoverCommand request, CancellationToken cancellationToken)
        {
            if (!database.Users.Any(x => x.Id == request.UserId))
            {
                throw new Exception(ExceptionStrings.UserNotFound);
            }

            if (database.Covers.Any(x => x.UserId == request.UserId))
            {
                throw new Exception(ExceptionStrings.EntityAlreadyExists);
            }

            Cover cover = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                FileName = string.Empty,
            };

            database.Covers.Add(cover);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("The cover was created ID: {Id}", cover.Id);

            return cover.Id;
        }
    }
}
