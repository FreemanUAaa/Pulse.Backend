using Pulse.Tracks.Core.Repositories;
using Pulse.Tracks.Grpc.Client.Services.Interfaces;

namespace Pulse.Tracks.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUsersGrpcService usersGrpcService;

        public UserRepository(IUsersGrpcService usersGrpcService) => this.usersGrpcService = usersGrpcService;

        public async Task<bool> IsExists(Guid userId) => await usersGrpcService.IsExists(userId);
    }
}
