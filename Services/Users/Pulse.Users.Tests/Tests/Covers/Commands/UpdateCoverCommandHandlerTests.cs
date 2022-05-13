using Microsoft.AspNetCore.Http;
using Pulse.Users.Application.Handlers.Covers.Commands.UpdateCover;
using Pulse.Users.Core.Models;
using Pulse.Users.Tests.Assets;
using Pulse.Users.Tests.Helpers.FormFiles;
using Pulse.Users.Tests.Tests.Covers.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Pulse.Users.Tests.Tests.Covers.Commands
{
    public class UpdateCoverCommandHandlerTests : BaseCoverCommandTests<UpdateCoverCommandHandler>
    {
        [Fact]
        public async void UpdateCoverCommandHandlerSuccess()
        {
            IFormFile file = FormFileFactory.Create(Photo.Bytes, "test-name.jpg");

            Cover cover = await AddCoverToDatabase(CreatedCover);
            UpdateCoverCommandHandler handler = new(Database, FileManager, Path, Logger);
            UpdateCoverCommand command = new()
            {
                UserId = cover.UserId,
                File = file,
            };


            await handler.Handle(command, CancellationToken.None);


            Cover newCover = await Database.Covers.FindAsync(cover.Id);
            string newFilePath = System.IO.Path.Combine(Path.Value.Cover, newCover.FileName);

            Assert.True(File.Exists(newFilePath));
            Assert.Equal(File.ReadAllBytes(newFilePath), Photo.Bytes);
        }

        [Fact]
        public async void UpdateCoverCommandHandlerFailOnWrongFileExtension()
        {
            IFormFile file = FormFileFactory.Create(Photo.Bytes, "test-name.wrong");

            Cover cover = await AddCoverToDatabase(CreatedCover);
            UpdateCoverCommandHandler handler = new(Database, FileManager, Path, Logger);
            UpdateCoverCommand command = new()
            {
                UserId = cover.UserId,
                File = file,
            };


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void UpdateCoverCommandHandlerFailOnWrongUserId()
        {
            IFormFile file = FormFileFactory.Create(Photo.Bytes, "test-name.wrong");

            UpdateCoverCommandHandler handler = new(Database, FileManager, Path, Logger);
            UpdateCoverCommand command = new();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }
    }
}
