using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Pulse.Users.Application.Handlers.Covers.Commands.UpdateCover
{
    public class UpdateCoverCommand : IRequest
    {
        public Guid UserId { get; set; }

        public IFormFile File { get; set; }
    }
}
