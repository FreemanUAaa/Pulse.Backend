using MassTransit;
using MassTransit.Mediator;
using Pulse.Messages.Actions;
using Pulse.Users.Application.Handlers.Covers.Commands.CreateCover;

namespace Pulse.Users.Consumers.Consumers
{
    public class CreateCoverConsumer : IConsumer<IUserCreatedAction>
    {
        private readonly IMediator mediator;

        private readonly ILogger<CreateCoverConsumer> logger;

        public CreateCoverConsumer(IMediator mediator, ILogger<CreateCoverConsumer> logger) =>
            (this.mediator, this.logger) = (mediator, logger);

        public async Task Consume(ConsumeContext<IUserCreatedAction> context)
        {
            CreateCoverCommand command = new() { UserId = context.Message.UserId };

            await mediator.Send(command);

            logger.LogInformation("Consumer => the cover was created user ID: {userId}", command.UserId);
        }
    }
}
