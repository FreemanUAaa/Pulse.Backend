using MediatR;

namespace Pulse.Users.Application.Handlers.Users.Queries.GetAccessToken
{
    public class LoginUserQuery : IRequest<LoginUserVm>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
