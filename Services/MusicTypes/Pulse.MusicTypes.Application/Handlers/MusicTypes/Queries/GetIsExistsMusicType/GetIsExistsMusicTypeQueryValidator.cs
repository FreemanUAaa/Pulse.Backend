using FluentValidation;

namespace Pulse.MusicTypes.Application.Handlers.MusicTypes.Queries.GetIsExistsMusicType
{
    public class GetIsExistsMusicTypeQueryValidator : AbstractValidator<GetIsExistsMusicTypeQuery>
    {
        public GetIsExistsMusicTypeQueryValidator()
        {
            RuleFor(x => x.MusicTypeIds).NotNull();

            RuleForEach(x => x.MusicTypeIds).NotEmpty();
        }
    }
}
