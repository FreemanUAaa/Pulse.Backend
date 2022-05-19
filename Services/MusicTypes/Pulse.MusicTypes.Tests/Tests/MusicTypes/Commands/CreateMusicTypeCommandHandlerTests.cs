using Pulse.MusicTypes.Application.Handlers.MusicTypes.Commands.CreateMusicType;
using Pulse.MusicTypes.Tests.Tests.MusicTypes.Base;
using System;
using System.Threading;
using Xunit;

namespace Pulse.MusicTypes.Tests.Tests.MusicTypes.Commands
{
    public class CreateMusicTypeCommandHandlerTests : BaseMusicTypeCommandTests<CreateMusicTypeCommandHandler>
    {
        [Fact]
        public async void CreateMusicTypeCommandHandlerSuccess()
        {
            CreateMusicTypeCommandHandler handler = new(Database, Logger);
            CreateMusicTypeCommand command = new() { Name = "new-test-name", };


            Guid musicTypeId = await handler.Handle(command, CancellationToken.None);


            Assert.NotNull(await Database.MusicTypes.FindAsync(new object[] { musicTypeId }, CancellationToken.None));
        }

        [Fact]
        public async void CreateMusicTypeCommandHandlerFailOnWrongName()
        {
            await SaveMusicType(MusicTypeEntity);
            CreateMusicTypeCommandHandler handler = new(Database, Logger);
            CreateMusicTypeCommand command = new() { Name = MusicTypeEntity.Name, };


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }
    }
}
