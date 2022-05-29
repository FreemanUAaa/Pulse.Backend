using FluentValidation;

namespace Pulse.Tracks.Application.Handlers.Tracks.Commands.CreateTrack
{
    public class CreateTrackCommandValidator : AbstractValidator<CreateTrackCommand>
    {
        public CreateTrackCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.Name).NotEmpty();

            RuleFor(x => x.Description).NotEmpty();

            RuleFor(x => x.Cover).NotNull();

            RuleFor(x => x.Song).NotNull();

            RuleFor(x => x.MusicTypeIds).NotNull();

            RuleForEach(x => x.MusicTypeIds).NotEmpty();
        }
    }
}
