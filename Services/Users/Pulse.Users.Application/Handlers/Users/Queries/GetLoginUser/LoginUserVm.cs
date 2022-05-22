using System;

namespace Pulse.Users.Application.Handlers.Users.Queries.GetLoginUser
{
    public class LoginUserVm
    {
        public Guid UserId { get; set; }

        public string AccessToken { get; set; }
    }
}
