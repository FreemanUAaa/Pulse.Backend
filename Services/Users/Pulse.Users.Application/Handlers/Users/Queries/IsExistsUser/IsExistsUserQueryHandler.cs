using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pulse.Users.Core.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Pulse.Users.Application.Handlers.Users.Queries.IsExistsUser
{
    public class IsExistsUserQueryHandler : IRequestHandler<IsExistsUserQuery, bool>
    {
        private readonly ILogger<IsExistsUserQueryHandler> logger;

        private readonly IDatabaseContext database;

        public IsExistsUserQueryHandler(IDatabaseContext database, ILogger<IsExistsUserQueryHandler> logger) =>
            (this.database, this.logger) = (database, logger);

        public async Task<bool> Handle(IsExistsUserQuery request, CancellationToken cancellationToken)
        {
            bool isExists = await database.Users.AnyAsync(x => x.Id == request.UserId, cancellationToken);

            logger.LogInformation("The user was be cacked");

            return isExists;
        }
    }
}
