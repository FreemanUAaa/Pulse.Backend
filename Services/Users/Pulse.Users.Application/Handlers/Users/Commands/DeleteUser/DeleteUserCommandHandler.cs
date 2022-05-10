using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Pulse.Users.Core.Cache;
using Pulse.Users.Core.Database;
using Pulse.Users.Core.Exceptions;
using Pulse.Users.Core.Models;
using Pulse.Users.Producers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pulse.Users.Application.Handlers.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly ILogger<DeleteUserCommandHandler> logger;

        private readonly IUserProducer userProducer;

        private readonly IDatabaseContext database;

        private readonly IDistributedCache cache;

        public DeleteUserCommandHandler(IDatabaseContext database, IUserProducer userProducer, IDistributedCache cache, ILogger<DeleteUserCommandHandler> logger) =>
            (this.database, this.userProducer, this.cache, this.logger) = (database, userProducer, cache, logger);

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User user = await database.Users.FindAsync(new object[] { request.UserId }, cancellationToken: cancellationToken);

            if (user == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            database.Users.Remove(user);
            await database.SaveChangesAsync(cancellationToken);

            await userProducer.PublishUserDeletedAction(user.Id);

            await cache.RemoveAsync(CacheContracts.GetUserKey(user.Id), cancellationToken);

            logger.LogInformation("The user was deleted ID: {Id}", user.Id);

            return Unit.Value;
        }
    }
}
