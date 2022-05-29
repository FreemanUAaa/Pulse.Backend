using FluentValidation;

namespace Pulse.Tracks.Application.Handlers.Tracks.Commands.DeleteTrack
{
    public class DeleteTrackCommandValidator : AbstractValidator<DeleteTrackCommand>
    {
        public DeleteTrackCommandValidator()
        {
            RuleFor(x => x.TrackId).NotEmpty();

            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
