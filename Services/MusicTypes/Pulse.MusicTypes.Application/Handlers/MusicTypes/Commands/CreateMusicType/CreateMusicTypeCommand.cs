using MediatR;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Commands.CreateMusicType
{
    public class CreateMusicTypeCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
    }
}
