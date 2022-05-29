using Grpc.Net.Client;
using Pulse.Tracks.Core.Options;
using Pulse.Tracks.Grpc.Client.Services.Interfaces;
using Pulse.Users.Grpc.Server;

namespace Pulse.Tracks.Grpc.Client.Services
{
    public class UsersGrpcService : IUsersGrpcService
    {
        private readonly GrpcOptions grpcOptions;

        public UsersGrpcService(GrpcOptions grpcOptions) => this.grpcOptions = grpcOptions;

        public async Task<bool> IsExists(Guid userId)
        {
            using GrpcChannel channel = GrpcChannel.ForAddress(grpcOptions.UserConnectionString);
            var client = new GrpcUsers.GrpcUsersClient(channel);

            UserIsExistsResponse response = await client.IsExistsAsync(new() { UserId = userId.ToString() });

            return response.IsExists;
        }
    }
}
