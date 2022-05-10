using MediatR;
using System;

namespace Pulse.Users.Application.Handlers.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}
