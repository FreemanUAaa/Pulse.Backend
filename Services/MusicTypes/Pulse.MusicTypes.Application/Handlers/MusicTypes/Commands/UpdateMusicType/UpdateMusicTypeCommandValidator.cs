using FluentValidation;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Commands.UpdateMusicType
{
    public class UpdateMusicTypeCommandValidator : AbstractValidator<UpdateMusicTypeCommand>
    {
        public UpdateMusicTypeCommandValidator()
        {
            RuleFor(x => x.MusicTypeId).NotEmpty();

            RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
        }
    }
}
