using Microsoft.Extensions.Options;
using Pulse.Users.Application.Handlers.Users.Queries.GetAccessToken;
using Pulse.Users.Core.Models;
using Pulse.Users.Core.Options;
using Pulse.Users.Tests.Tests.Users.Base;
using System;
using System.Threading;
using Xunit;

namespace Pulse.Users.Tests.Tests.Users.Queries
{
    public class GetAccessTokenQueryHandlerTests : BaseUserQueryTests<LoginUserQueryHandler>
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
            LoginUserQueryHandler handler = new(Database, auth, Logger);
            LoginUserQuery query = new()
            {
                Email = user.Email,
                Password = Password,
            };


            LoginUserVm vm = await handler.Handle(query, CancellationToken.None);


            Assert.False(string.IsNullOrEmpty(vm.AccessToken));
            Assert.Equal(user.Id, vm.UserId);
        }

        [Fact]
        public async void GetAccessTokenQueryHandlerFailOnWrongEmailOrPassword()
        {
            LoginUserQueryHandler handler = new(Database, auth, Logger);
            LoginUserQuery query = new();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(query, CancellationToken.None));
        }
    }
}
