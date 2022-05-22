using MediatR;

namespace Pulse.Users.Application.Handlers.Users.Queries.GetLoginUser
{
    public class LoginUserQuery : IRequest<LoginUserVm>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
