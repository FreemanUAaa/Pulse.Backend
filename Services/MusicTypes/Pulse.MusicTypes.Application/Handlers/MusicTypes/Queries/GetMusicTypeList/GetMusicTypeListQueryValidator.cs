using FluentValidation;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Queries.GetMusicTypeList
{
    public class GetMusicTypeListQueryValidator : AbstractValidator<GetMusicTypeListQuery>
    {
        public GetMusicTypeListQueryValidator()
        {
            RuleFor(x => x.MusicTypeIds).NotNull();

            RuleForEach(x => x.MusicTypeIds).NotEmpty();
        }
    }
}
