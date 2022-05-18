using FluentValidation;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Commands.DeleteMusicType
{
    public class DeleteMusicTypeCommandValidator : AbstractValidator<DeleteMusicTypeCommand>
    {
        public DeleteMusicTypeCommandValidator()
        {
            RuleFor(x => x.MusicTypeId).NotEmpty();
        }
    }
}
