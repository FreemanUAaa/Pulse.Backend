using MediatR;
using Microsoft.AspNetCore.Http;

namespace Pulse.Tracks.Application.Handlers.Tracks.Commands.UpdateTrack
{
    public class UpdateTrackCommand : IRequest
    {
        public Guid TrackId { get; set; }

        public Guid UserId { get; set; }

        public int Duration { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public IFormFile? Cover { get; set; }

        public IFormFile? Song { get; set; }

        public List<Guid>? MusicTypeIds { get; set; }
    }
}
