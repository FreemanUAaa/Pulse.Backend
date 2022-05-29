namespace Pulse.Tracks.Grpc.Client.Services.Interfaces
{
    public interface IUsersGrpcService
    {
        Task<bool> IsExists(Guid userId);
    }
}
