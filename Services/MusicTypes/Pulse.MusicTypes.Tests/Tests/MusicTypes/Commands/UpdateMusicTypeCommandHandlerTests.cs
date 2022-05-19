using Pulse.MusicTypes.Application.Handlers.MusicTypes.Commands.UpdateMusicType;
using Pulse.MusicTypes.Core.Models;
using Pulse.MusicTypes.Tests.Tests.MusicTypes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Pulse.MusicTypes.Tests.Tests.MusicTypes.Commands
{
    public class UpdateMusicTypeCommandHandlerTests : BaseMusicTypeCommandTests<UpdateMusicTypeCommandHandler>
    {
        [Fact]
        public async void UpdateMusicTypeCommandHandlerSuccess()
        {
            await SaveMusicType(MusicTypeEntity);
            UpdateMusicTypeCommandHandler handler = new(Database, Logger);
            UpdateMusicTypeCommand command = new()
            {
                MusicTypeId = MusicTypeEntity.Id,
                Name = "new-test-name",
            };


            await handler.Handle(command, CancellationToken.None);


            MusicType? updatedMusicType = await Database.MusicTypes.FindAsync(new object[] { MusicTypeEntity.Id }, CancellationToken.None);

            Assert.NotNull(updatedMusicType);
            Assert.Equal(updatedMusicType?.Name, command.Name);
        }

        [Fact]
        public async void UpdateMusicTypeCommandHandlerFailOnWrongMusicTypeId()
        {
            UpdateMusicTypeCommandHandler handler = new(Database, Logger);
            UpdateMusicTypeCommand command = new();


            await Assert.ThrowsAsync<Exception>(async () =>
               await handler.Handle(command, CancellationToken.None));
        }
    }
}
