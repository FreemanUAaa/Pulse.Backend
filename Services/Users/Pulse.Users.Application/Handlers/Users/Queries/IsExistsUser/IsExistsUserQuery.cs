using MediatR;
using System;

namespace Pulse.Users.Application.Handlers.Users.Queries.IsExistsUser
{
    public class IsExistsUserQuery : IRequest<bool>
    {
        public Guid UserId { get; set; }
    }
}
