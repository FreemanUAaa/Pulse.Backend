using FluentValidation;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Commands.CreateMusicType
{
    public class CreateMusicTypeCommandValidator : AbstractValidator<CreateMusicTypeCommand>
    {
        public CreateMusicTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
        }
    }
}
