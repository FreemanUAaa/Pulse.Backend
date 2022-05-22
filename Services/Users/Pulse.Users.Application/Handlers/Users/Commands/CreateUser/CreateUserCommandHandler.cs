using MediatR;
using Microsoft.Extensions.Logging;
using Pulse.Users.Core.Database;
using Pulse.Users.Core.Exceptions;
using Pulse.Users.Core.Helpers.Passwords;
using Pulse.Users.Core.Helpers.Salts;
using Pulse.Users.Core.Models;
using Pulse.Users.Producers.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pulse.Users.Application.Handlers.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly ILogger<CreateUserCommandHandler> logger;

        private readonly IUserProducer userProducer;

        private readonly IDatabaseContext database;

        public CreateUserCommandHandler(IDatabaseContext database, IUserProducer userProducer, ILogger<CreateUserCommandHandler> logger) =>
            (this.database, this.userProducer, this.logger) = (database, userProducer, logger);

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (database.Users.Any(x => x.Email == request.Email))
            {
                throw new Exception(ExceptionStrings.EmailIsBusy);
            }

            byte[] salt = Salt.Generate();
            string hash = Hasher.HashPassword(request.Password, salt);

            User user = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                MusicTypes = new(),
		PasswordHash = hash,
		PasswordSalt = salt,
                Location = "",
                Website = "",
            };

            database.Users.Add(user);
            await database.SaveChangesAsync(cancellationToken);

            await userProducer.PublishUserCreatedAction(user.Id);

            logger.LogInformation("The user was created ID: {Id}", user.Id);

            return user.Id;
        }
    }
}
