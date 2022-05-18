using MediatR;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Commands.DeleteMusicType
{
    public class DeleteMusicTypeCommand : IRequest
    {
        public Guid MusicTypeId { get; set; }
    }
}
