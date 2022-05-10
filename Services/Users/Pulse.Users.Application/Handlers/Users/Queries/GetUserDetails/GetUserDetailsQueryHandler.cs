using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Pulse.Users.Core.Database;
using Pulse.Users.Core.Exceptions;
using Pulse.Users.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pulse.Users.Application.Handlers.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, GetUserDetailsVm>
    {
        private readonly ILogger<GetUserDetailsQueryHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IMapper mapper;

        public GetUserDetailsQueryHandler(IDatabaseContext database, IMapper mapper, ILogger<GetUserDetailsQueryHandler> logger) =>
            (this.database, this.mapper, this.logger) = (database, mapper, logger);

        public async Task<GetUserDetailsVm> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            User user = await database.Users.FindAsync(new object[] { request.UserId }, cancellationToken: cancellationToken);

            if (user == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            GetUserDetailsVm vm = mapper.Map<GetUserDetailsVm>(user);

            logger.LogInformation("The user was received");

            return vm;
        }
    }
}
