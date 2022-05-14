using MediatR;
using System;

namespace Pulse.Users.Application.Handlers.Covers.Queries.GetCoverBytes
{
    public class GetCoverBytesQuery : IRequest<GetCoverBytesVm>
    {
        public Guid UserId { get; set; }
    }
}
