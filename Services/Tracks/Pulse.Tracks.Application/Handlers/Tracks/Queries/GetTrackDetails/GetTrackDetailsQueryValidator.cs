using FluentValidation;

namespace Pulse.Tracks.Application.Handlers.Tracks.Queries.GetTrackDetails
{
    public class GetTrackDetailsQueryValidator : AbstractValidator<GetTrackDetailsQuery>
    {
        public GetTrackDetailsQueryValidator()
        {
            RuleFor(x => x.TrackId).NotEmpty();
        }
    }
}
