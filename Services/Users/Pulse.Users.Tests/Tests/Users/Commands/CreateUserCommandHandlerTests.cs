using Pulse.Users.Application.Handlers.Users.Commands.CreateUser;
using Pulse.Users.Core.Models;
using Pulse.Users.Tests.Tests.Users.Base;
using System;
using System.Threading;
using Xunit;

namespace Pulse.Users.Tests.Tests.Users.Commands
{
    public class CreateUserCommandHandlerTests : BaseUserCommandTests<CreateUserCommandHandler>
    {
        [Fact]
        public async void CreateUserCommandHandlerSuccess()
        {
            CreateUserCommandHandler handler = new(Database, UserProducer, Logger);
            CreateUserCommand command = new()
            {
                Email = "test-email",
                Name = "test-name",
                Password = "test-password",
            };


            Guid userId = await handler.Handle(command, CancellationToken.None);


            Assert.NotNull(await Database.Users.FindAsync(userId));
        }

        [Fact]
        public async void CreateUserCommandHandlerFailOnWrongEmail()
        {
            User user = await AddUserToDatabase(CreatedUser);
            CreateUserCommandHandler handler = new(Database, UserProducer, Logger);
            CreateUserCommand command = new() { Email = user.Email, };


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }
    }
}
