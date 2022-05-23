using FluentValidation;

namespace Pulse.Users.Application.Handlers.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(4);

            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Role).NotEmpty().Must(x => x == "admin" || x == "user");

            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }
}
