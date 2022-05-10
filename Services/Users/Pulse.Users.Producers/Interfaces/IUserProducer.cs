namespace Pulse.Users.Producers.Interfaces
{
    public interface IUserProducer
    {
        Task PublishUserCreatedAction(Guid userId);

        Task PublishUserDeletedAction(Guid userId);
    }
}
