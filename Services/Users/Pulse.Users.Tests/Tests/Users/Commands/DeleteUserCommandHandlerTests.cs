using Pulse.Users.Application.Handlers.Users.Commands.DeleteUser;
using Pulse.Users.Core.Models;
using Pulse.Users.Tests.Tests.Users.Base;
using System;
using System.Threading;
using Xunit;

namespace Pulse.Users.Tests.Tests.Users.Commands
{
    public class DeleteUserCommandHandlerTests : BaseUserCommandTests<DeleteUserCommandHandler>
    {
        [Fact]
        public async void DeleteUserCommandHandlerSuccess()
        {
            User user = await AddUserToDatabase(CreatedUser);
            DeleteUserCommandHandler handler = new(Database, UserProducer, Cache, Logger);
            DeleteUserCommand command = new() { UserId = user.Id };


            await handler.Handle(command, CancellationToken.None);


            Assert.Null(await Database.Users.FindAsync(user.Id));
        }

        [Fact]
        public async void DeleteUserCommandHandlerFailOnWrongId()
        {
            DeleteUserCommandHandler handler = new(Database, UserProducer, Cache, Logger);
            DeleteUserCommand command = new();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }
    }
}
