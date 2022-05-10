using FluentValidation;

namespace Pulse.Users.Application.Handlers.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQueryValidator : AbstractValidator<GetUserDetailsQuery>
    {
        public GetUserDetailsQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
