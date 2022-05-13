using MassTransit;
using MediatR;
using Pulse.Messages.Actions;
using Pulse.Users.Application.Handlers.Covers.Commands.DeleteCover;

namespace Pulse.Users.Consumers.Consumers
{
    public class DeleteCoverConsumer : IConsumer<IUserDeletedAction>
    {
        private readonly IMediator mediator;

        private readonly ILogger<DeleteCoverConsumer> logger;

        public DeleteCoverConsumer(IMediator mediator, ILogger<DeleteCoverConsumer> logger) =>
            (this.mediator, this.logger) = (mediator, logger);

        public async Task Consume(ConsumeContext<IUserDeletedAction> context)
        {
            DeleteCoverCommand command = new() { UserId = context.Message.UserId };

            await mediator.Send(command);

            logger.LogInformation("Consumer => the cover was deleted user ID: {userId}", command.UserId);
        }
    }
}
