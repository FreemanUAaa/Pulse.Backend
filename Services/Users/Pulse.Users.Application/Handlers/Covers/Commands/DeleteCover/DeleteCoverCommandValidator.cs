using FluentValidation;

namespace Pulse.Users.Application.Handlers.Covers.Commands.DeleteCover
{
    public class DeleteCoverCommandValidator : AbstractValidator<DeleteCoverCommand>
    {
        public DeleteCoverCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
