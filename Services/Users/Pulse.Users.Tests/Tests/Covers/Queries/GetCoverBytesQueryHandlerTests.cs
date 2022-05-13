using Pulse.Users.Application.Handlers.Covers.Queries.GetCoverBytes;
using Pulse.Users.Core.Models;
using Pulse.Users.Tests.Assets;
using Pulse.Users.Tests.Tests.Covers.Base;
using System;
using System.Threading;
using Xunit;

namespace Pulse.Users.Tests.Tests.Covers.Queries
{
    public class GetCoverBytesQueryHandlerTests : BaseCoverQueryTests<GetCoverBytesQueryHandler>
    {
        [Fact]
        public async void GetCoverBytesQueryHandlerSuccess()
        {
            Cover cover = await AddCoverToDatabase(CreatedCover);
            GetCoverBytesQueryHandler handler = new(Database, Path, Logger);
            GetCoverBytesQuery query = new() { UserId = cover.UserId };


            byte[] vm = await handler.Handle(query, CancellationToken.None);


            Assert.Equal(vm, Photo.Bytes);
        }

        [Fact]
        public async void GetCoverBytesQueryHandlerFailOnWrongUserId()
        {
            GetCoverBytesQueryHandler handler = new(Database, Path, Logger);
            GetCoverBytesQuery query = new();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(query, CancellationToken.None));
        }
    }
}
