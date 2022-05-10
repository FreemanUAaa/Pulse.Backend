using MediatR;
using System;
using System.Collections.Generic;

namespace Pulse.Users.Application.Handlers.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Website { get; set; }

        public List<Guid> MusicTypeIds { get; set; }
    }
}
