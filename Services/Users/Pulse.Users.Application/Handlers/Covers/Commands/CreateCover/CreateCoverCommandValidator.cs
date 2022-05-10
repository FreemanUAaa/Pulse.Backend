using FluentValidation;

namespace Pulse.Users.Application.Handlers.Covers.Commands.CreateCover
{
    public class CreateCoverCommandValidator : AbstractValidator<CreateCoverCommand>
    {
        public CreateCoverCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.File).NotNull();
        }
    }
}
