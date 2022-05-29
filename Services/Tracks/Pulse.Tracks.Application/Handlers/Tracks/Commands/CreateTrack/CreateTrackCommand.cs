using MediatR;
using Microsoft.AspNetCore.Http;

namespace Pulse.Tracks.Application.Handlers.Tracks.Commands.CreateTrack
{
    public class CreateTrackCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }

        public int Duration { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public IFormFile Cover { get; set; } = null!;

        public IFormFile Song { get; set; } = null!;

        public List<Guid> MusicTypeIds { get; set; } = new();
    }
}
