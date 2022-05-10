using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Pulse.Users.Core.Cache;
using Pulse.Users.Core.Database;
using Pulse.Users.Core.Exceptions;
using Pulse.Users.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pulse.Users.Application.Handlers.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly ILogger<UpdateUserCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IDistributedCache cache;

        public UpdateUserCommandHandler(IDatabaseContext database, IDistributedCache cache, ILogger<UpdateUserCommandHandler> logger) =>
            (this.database, this.cache, this.logger) = (database, cache, logger);

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await database.Users.FindAsync(new object[] { request.UserId }, cancellationToken: cancellationToken);
        
            if (user == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            (user.Name, user.Website, user.Location, user.MusicTypeIds) = 
                (request.Name, request.Website, request.Location, request.MusicTypeIds);

            database.Users.Update(user);
            await database.SaveChangesAsync(cancellationToken);

            await cache.RemoveAsync(CacheContracts.GetUserKey(user.Id), cancellationToken);

            logger.LogInformation("The user was updated ID: {Id}", user.Id);

            return Unit.Value;
        }
    }
}
