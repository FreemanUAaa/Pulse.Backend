using FluentValidation;

namespace Pulse.Users.Application.Handlers.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.Website).NotEmpty();

            RuleFor(x => x.Location).NotEmpty();

            RuleFor(x => x.MusicTypeIds).NotEmpty();

            RuleForEach(x => x.MusicTypeIds).NotEmpty();
        }
    }
}
