using FluentValidation;

namespace Pulse.Users.Application.Handlers.Covers.Commands.UpdateCover
{
    public class UpdateCoverCommandValidator : AbstractValidator<UpdateCoverCommand>
    {
        public UpdateCoverCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.File).NotNull();
        }
    }
}
