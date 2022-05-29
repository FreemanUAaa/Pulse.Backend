using MediatR;

namespace Pulse.Tracks.Application.Handlers.Tracks.Commands.DeleteTrack
{
    public class DeleteTrackCommand : IRequest
    {
        public Guid TrackId { get; set; }

        public Guid UserId { get; set; }
    }
}
