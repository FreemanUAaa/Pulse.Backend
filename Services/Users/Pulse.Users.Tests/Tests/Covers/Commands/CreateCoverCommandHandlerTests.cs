using Pulse.Users.Application.Handlers.Covers.Commands.CreateCover;
using Pulse.Users.Core.Models;
using Pulse.Users.Tests.Tests.Covers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Pulse.Users.Tests.Tests.Covers.Commands
{
    public class CreateCoverCommandHandlerTests : BaseCoverCommandTests<CreateCoverCommandHandler>
    {
        [Fact]
        public async void CreateCoverCommandHandlerSuccess()
        {
            CreateCoverCommandHandler handler = new(Database, Logger);
            CreateCoverCommand command = new() { UserId = CreatedUser.Id };


            Guid coverId = await handler.Handle(command, CancellationToken.None);


            Assert.NotNull(await Database.Covers.FindAsync(coverId));
        }

        [Fact]
        public async void CreateCoverCommandHandlerFailOnWrongUserId()
        {
            CreateCoverCommandHandler handler = new(Database, Logger);
            CreateCoverCommand command = new();

            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void CreateCoverCommandHandlerFailOnWrongCoverId()
        {
            Cover cover = await AddCoverToDatabase(CreatedCover);
            CreateCoverCommandHandler handler = new(Database, Logger);
            CreateCoverCommand command = new() { UserId = CreatedUser.Id };


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }
    }
}
