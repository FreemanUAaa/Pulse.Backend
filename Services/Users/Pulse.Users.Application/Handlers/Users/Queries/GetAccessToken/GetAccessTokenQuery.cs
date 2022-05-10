using MediatR;

namespace Pulse.Users.Application.Handlers.Users.Queries.GetAccessToken
{
    public class GetAccessTokenQuery : IRequest<string>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
