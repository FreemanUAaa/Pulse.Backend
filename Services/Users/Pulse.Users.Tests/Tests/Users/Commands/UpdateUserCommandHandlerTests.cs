using Pulse.Users.Application.Handlers.Users.Commands.UpdateUser;
using Pulse.Users.Core.Models;
using Pulse.Users.Tests.Tests.Users.Base;
using Shouldly;
using System;
using System.Threading;
using Xunit;

namespace Pulse.Users.Tests.Tests.Users.Commands
{
    public class UpdateUserCommandHandlerTests : BaseUserCommandTests<UpdateUserCommandHandler>
    {
        [Fact]
        public async void UpdateUserCommandHandlerSuccess()
        {
            User user = await AddUserToDatabase(CreatedUser);
            UpdateUserCommandHandler handler = new(Database, Cache, Logger);
            UpdateUserCommand command = new()
            {
                Location = "new-location",
                Website = "new-website",
                Name = "new-website",
                UserId = user.Id,
                MusicTypeIds = new(),
            };


            await handler.Handle(command, CancellationToken.None);


            User updatedUser = await Database.Users.FindAsync(user.Id);

            Assert.NotNull(updatedUser);
            user.Location.ShouldBe(command.Location);
            user.Website.ShouldBe(command.Website);
            user.Name.ShouldBe(command.Name);
            user.MusicTypes.Count.ShouldBe(command.MusicTypeIds.Count);
        }

        [Fact]
        public async void UpdateUserCommandHandlerFailOnWrongId()
        {
            UpdateUserCommandHandler handler = new(Database, Cache, Logger);
            UpdateUserCommand command = new();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }
    }
}
