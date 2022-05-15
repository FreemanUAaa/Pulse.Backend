using MediatR;
using Microsoft.AspNetCore.Http;

namespace Pulse.Tracks.Application.Handlers.Tracks.Commands.CreateTrack
{
    public class CreateTrackCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile Cover { get; set; }

        public IFormFile File { get; set; }

        public List<Guid> MusicTypeIds { get; set; }

        public CreateTrackCommand() =>
            (Name, Description, Cover, File, MusicTypeIds) = (string.Empty, string.Empty, null!, null!, null!);
    }
}
