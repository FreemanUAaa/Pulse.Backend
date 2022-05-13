using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Pulse.Users.Application.Handlers.Covers.Commands.CreateCover
{
    public class CreateCoverCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
    }
}
