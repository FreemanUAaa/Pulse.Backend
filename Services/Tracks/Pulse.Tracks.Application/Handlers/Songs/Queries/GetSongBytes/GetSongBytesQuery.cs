using MediatR;

namespace Pulse.Tracks.Application.Handlers.Songs.Queries.GetSongBytes
{
    public class GetSongBytesQuery : IRequest<GetSongBytesQueryVm>
    {
        public Guid TrackId { get; set; }
    }
}
