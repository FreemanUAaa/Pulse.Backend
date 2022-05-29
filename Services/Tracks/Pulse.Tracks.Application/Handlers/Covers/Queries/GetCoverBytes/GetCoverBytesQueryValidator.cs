using FluentValidation;

namespace Pulse.Tracks.Application.Handlers.Covers.Queries.GetCoverBytes
{
    public class GetCoverBytesQueryValidator : AbstractValidator<GetCoverBytesQuery>
    {
        public GetCoverBytesQueryValidator()
        {
            RuleFor(x => x.TrackId).NotEmpty();
        }
    }
}
