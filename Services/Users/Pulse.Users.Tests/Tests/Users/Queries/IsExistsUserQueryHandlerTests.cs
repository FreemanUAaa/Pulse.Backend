using Pulse.Users.Application.Handlers.Users.Queries.IsExistsUser;
using Pulse.Users.Core.Models;
using Pulse.Users.Tests.Tests.Users.Base;
using System.Threading;
using Xunit;

namespace Pulse.Users.Tests.Tests.Users.Queries
{
    public class IsExistsUserQueryHandlerTests : BaseUserCommandTests<IsExistsUserQueryHandler>
    {
        [Fact]
        public async void IsExistsUserQueryHandlerSuccess()
        {
            User user = await AddUserToDatabase(CreatedUser);
            IsExistsUserQueryHandler handler = new(Database, Logger);
            IsExistsUserQuery query = new() { UserId = user.Id };


            bool isExists = await handler.Handle(query, CancellationToken.None);


            Assert.True(isExists);
        }
    }
}
