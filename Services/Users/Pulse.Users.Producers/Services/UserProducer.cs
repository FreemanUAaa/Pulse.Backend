using MassTransit;
using Pulse.Messages.Actions;
using Pulse.Users.Producers.Interfaces;

namespace Pulse.Users.Producers.Services
{
    public class UserProducer : IUserProducer
    {
        private readonly IPublishEndpoint endpoint;

        public UserProducer(IPublishEndpoint endpoint) => this.endpoint = endpoint;

        public async Task PublishUserCreatedAction(Guid userId)
        {
            await endpoint.Publish<IUserCreatedAction>(new { UserId = userId });
        }

        public async Task PublishUserDeletedAction(Guid userId)
        {
            await endpoint.Publish<IUserDeletedAction>(new { UserId = userId });
        }
    }
}
