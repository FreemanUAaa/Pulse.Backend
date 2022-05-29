using FluentValidation;

namespace Pulse.Tracks.Application.Handlers.Songs.Queries.GetSongBytes
{
    public class GetSongBytesQueryValidator : AbstractValidator<GetSongBytesQuery>
    {
        public GetSongBytesQueryValidator()
        {
            RuleFor(x => x.TrackId).NotEmpty();
        }
    }
}
