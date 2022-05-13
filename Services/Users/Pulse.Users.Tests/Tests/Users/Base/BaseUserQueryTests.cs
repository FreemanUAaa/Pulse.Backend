using Pulse.Users.Core.Helpers.Passwords;
using Pulse.Users.Core.Helpers.Salts;
using Pulse.Users.Core.Models;
using Pulse.Users.Tests.Tests.Base;
using System;
using System.Threading.Tasks;

namespace Pulse.Users.Tests.Tests.Users.Base
{
    public class BaseUserQueryTests<TLogger> : BaseQueryTests<TLogger> where TLogger : class
    {
        public readonly string Password;

        public readonly User CreatedUser;

        public BaseUserQueryTests() : base()
        {
            Password = "test-password";

            byte[] salt = Salt.Generate();

            string hash = Hasher.HashPassword(Password, salt);

            CreatedUser = new()
            {
                Id = Guid.NewGuid(),
                Email = "test-email",
                Name = "test-name",
                PasswordHash = hash,
                PasswordSalt = salt,
                Location = "test-location",
                MusicTypes = new(),
                Website = "test-website",
            };
        }

        public async Task<User> AddUserToDatabase(User user)
        {
            if (user == null)
            {
                throw new ArgumentException("The user can not be null");
            }

            Database.Users.Add(user);
            await Database.SaveChangesAsync();

            return user;
        }
    }
}
