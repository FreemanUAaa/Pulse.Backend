namespace Pulse.Tracks.Core.Repositories
{
    public interface IUserRepository
    {
        Task<bool> IsExists(Guid userId);
    }
}
