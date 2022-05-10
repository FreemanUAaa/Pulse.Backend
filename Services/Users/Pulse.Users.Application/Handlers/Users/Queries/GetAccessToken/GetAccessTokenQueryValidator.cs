using FluentValidation;

namespace Pulse.Users.Application.Handlers.Users.Queries.GetAccessToken
{
    public class GetAccessTokenQueryValidator : AbstractValidator<GetAccessTokenQuery>
    {
        public GetAccessTokenQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
