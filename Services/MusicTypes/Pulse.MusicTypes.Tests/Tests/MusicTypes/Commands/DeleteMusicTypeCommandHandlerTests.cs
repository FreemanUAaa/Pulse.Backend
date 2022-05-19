using Pulse.MusicTypes.Application.Handlers.MusicTypes.Commands.DeleteMusicType;
using Pulse.MusicTypes.Tests.Tests.MusicTypes.Base;
using System;
using System.Threading;
using Xunit;

namespace Pulse.MusicTypes.Tests.Tests.MusicTypes.Commands
{
    public class DeleteMusicTypeCommandHandlerTests : BaseMusicTypeCommandTests<DeleteMusicTypeCommandHandler>
    {
        [Fact]
        public async void DeleteMusicTypeCommandHandlerSuccess()
        {
            await SaveMusicType(MusicTypeEntity);
            DeleteMusicTypeCommandHandler handler = new(Database, Logger);
            DeleteMusicTypeCommand command = new() { MusicTypeId = MusicTypeEntity.Id };


            await handler.Handle(command, CancellationToken.None);


            Assert.Null(await Database.MusicTypes.FindAsync(new object[] { MusicTypeEntity.Id }, CancellationToken.None)); ;
        }

        [Fact]
        public async void DeleteMusicTypeCommandHandlerFailOnWrongMusicTypeId()
        {
            DeleteMusicTypeCommandHandler handler = new(Database, Logger);
            DeleteMusicTypeCommand command = new();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }
    }
}
