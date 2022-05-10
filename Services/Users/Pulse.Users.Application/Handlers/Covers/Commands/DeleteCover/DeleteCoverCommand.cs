using MediatR;
using System;

namespace Pulse.Users.Application.Handlers.Covers.Commands.DeleteCover
{
    public class DeleteCoverCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}
