using MediatR;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Commands.UpdateMusicType
{
    public class UpdateMusicTypeCommand : IRequest
    {
        public Guid MusicTypeId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
