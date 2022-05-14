using FluentValidation;

namespace Pulse.Users.Application.Handlers.Users.Queries.IsExistsUser
{
    public class IsExistsUserQueryValidator : AbstractValidator<IsExistsUserQuery>
    {
        public IsExistsUserQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
