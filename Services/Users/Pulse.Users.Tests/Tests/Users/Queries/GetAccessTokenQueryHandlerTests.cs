using Microsoft.Extensions.Options;
using Pulse.Users.Application.Handlers.Users.Queries.GetAccessToken;
using Pulse.Users.Core.Models;
using Pulse.Users.Core.Options;
using Pulse.Users.Tests.Tests.Users.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Pulse.Users.Tests.Tests.Users.Queries
{
    public class GetAccessTokenQueryHandlerTests : BaseUserQueryTests<GetAccessTokenQueryHandler>
    {
        private readonly IOptions<AuthOptions> auth;

        public GetAccessTokenQueryHandlerTests() =>
            auth = Options.Create(new AuthOptions()
            {
                Key = "mysupersecret_secretkey!123",
                Audience = "test-audience",
                Issuer = "test-Issuer",
                Lifetime = 250,
            });

        [Fact]
        public async void GetAccessTokenQueryHandlerSuccess()
        {
            User user = await AddUserToDatabase(CreatedUser);
            GetAccessTokenQueryHandler handler = new(Database, auth, Logger);
            GetAccessTokenQuery query = new()
            {
                Email = user.Email,
                Password = Password,
            };


            string token = await handler.Handle(query, CancellationToken.None);


            Assert.False(string.IsNullOrEmpty(token));
        }

        [Fact]
        public async void GetAccessTokenQueryHandlerFailOnWrongEmailOrPassword()
        {
            GetAccessTokenQueryHandler handler = new(Database, auth, Logger);
            GetAccessTokenQuery query = new();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(query, CancellationToken.None));
        }
    }
}
