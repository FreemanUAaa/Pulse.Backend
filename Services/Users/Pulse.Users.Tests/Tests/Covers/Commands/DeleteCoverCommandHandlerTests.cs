using Pulse.Users.Application.Handlers.Covers.Commands.DeleteCover;
using Pulse.Users.Core.Models;
using Pulse.Users.Tests.Tests.Covers.Base;
using System;
using System.IO;
using System.Threading;
using Xunit;

namespace Pulse.Users.Tests.Tests.Covers.Commands
{
    public class DeleteCoverCommandHandlerTests : BaseCoverCommandTests<DeleteCoverCommandHandler>
    {
        [Fact]
        public async void DeleteCoverCommandHandlerSuccess()
        {
            Cover cover = await AddCoverToDatabase(CreatedCover);
            DeleteCoverCommandHandler handler = new(Database, FileManager, Path, Logger);
            DeleteCoverCommand command = new() { UserId = cover.UserId };


            await handler.Handle(command, CancellationToken.None);


            Assert.Null(await Database.Covers.FindAsync(cover.Id));
            Assert.False(File.Exists(System.IO.Path.Combine(Path.Value.Cover, cover.FileName)));
        }

        [Fact]
        public async void DeleteCoverCommandHandlerFailOnWrongUserId()
        {
            DeleteCoverCommandHandler handler = new(Database, FileManager, Path, Logger);
            DeleteCoverCommand command = new();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }
    }
}
