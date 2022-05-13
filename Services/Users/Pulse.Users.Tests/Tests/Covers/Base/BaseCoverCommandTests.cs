using Pulse.Users.Core.Helpers.Files;
using Pulse.Users.Core.Helpers.Passwords;
using Pulse.Users.Core.Helpers.Salts;
using Pulse.Users.Core.Models;
using Pulse.Users.Tests.Assets;
using Pulse.Users.Tests.Tests.Base;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Pulse.Users.Tests.Tests.Covers.Base
{
    public abstract class BaseCoverCommandTests<TLogger> : BaseCommandTests<TLogger> where TLogger: class
    {
        public readonly string Password;

        public readonly User CreatedUser;

        public readonly Cover CreatedCover;

        public BaseCoverCommandTests() : base()
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

            string newFileName = FileNameGenerator.GenerateUniqueFileName(Path.Value.Cover, ".jpg", 10);

            string newFilePath = System.IO.Path.Combine(Path.Value.Cover, newFileName);

            CreatedCover = new()
            {
                Id = Guid.NewGuid(),
                UserId = CreatedUser.Id,
                FileName = newFileName
            };

            File.WriteAllBytes(newFilePath, Photo.Bytes);
        }

        public async Task<Cover> AddCoverToDatabase(Cover cover)
        {
            if (cover == null)
            {
                throw new ArgumentException("The cover can not be null");
            }

            Database.Covers.Add(cover);
            await Database.SaveChangesAsync();

            return cover;
        }
    }
}
