using Pulse.Users.Application.Handlers.Users.Queries.GetUserDetails;
using Pulse.Users.Core.Models;
using Pulse.Users.Tests.Tests.Users.Base;
using System;
using System.Threading;
using Xunit;

namespace Pulse.Users.Tests.Tests.Users.Queries
{
    public class GetUserDetailsQueryHandlerTests : BaseUserQueryTests<GetUserDetailsQueryHandler>
    {
        [Fact]
        public async void GetUserDetailsQueryHandlerSuccess()
        {
            User user = await AddUserToDatabase(CreatedUser);
            GetUserDetailsQueryHandler handler = new(Database, Mapper, Logger);
            GetUserDetailsQuery query = new() { UserId = user.Id };


            GetUserDetailsVm vm = await handler.Handle(query, CancellationToken.None);


            Assert.NotNull(vm);
        }

        [Fact]
        public async void GetUserDetailsQueryHandlerFailOnWrongId()
        {
            GetUserDetailsQueryHandler handler = new(Database, Mapper, Logger);
            GetUserDetailsQuery query = new();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(query, CancellationToken.None));
        }
    }
}
