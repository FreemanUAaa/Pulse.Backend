using Grpc.Core;
using MediatR;
using Pulse.Users.Application.Handlers.Users.Queries.IsExistsUser;

namespace Pulse.Users.Grpc.Server.Services
{
    public class UsersGrpcService : GrpcUsers.GrpcUsersBase
    {
        private readonly ILogger<UsersGrpcService> logger;

        private readonly IMediator mediator;

        public UsersGrpcService(IMediator mediator, ILogger<UsersGrpcService> logger) =>
            (this.mediator, this.logger) = (mediator, logger);
        
        public override async Task<UserIsExistsResponse> IsExists(UserIsExistsRequest request, ServerCallContext context)
        {
            IsExistsUserQuery query = new() { UserId = Guid.Parse(request.UserId) };

            bool isExists = await mediator.Send(query);

            logger.LogInformation("Grpc => call is user exists");

            return new UserIsExistsResponse() { IsExists = isExists };
        }
    }
}
