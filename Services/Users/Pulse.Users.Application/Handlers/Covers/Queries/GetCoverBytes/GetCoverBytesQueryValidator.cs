using FluentValidation;

namespace Pulse.Users.Application.Handlers.Covers.Queries.GetCoverBytes
{
    public class GetCoverBytesQueryValidator : AbstractValidator<GetCoverBytesQuery>
    {
        public GetCoverBytesQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
