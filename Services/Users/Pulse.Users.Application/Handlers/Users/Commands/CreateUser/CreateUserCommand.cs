using MediatR;
using System;

namespace Pulse.Users.Application.Handlers.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
