using MediatR;

namespace Pulse.Tracks.Application.Handlers.Covers.Queries.GetCoverBytes
{
    public class GetCoverBytesQuery : IRequest<GetCoverBytesQueryVm>
    {
        public Guid TrackId { get; set; }
    }
}
