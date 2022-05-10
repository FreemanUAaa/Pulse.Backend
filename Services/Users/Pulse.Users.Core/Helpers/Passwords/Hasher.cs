using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Pulse.Users.Core.Helpers.Passwords
{
    public static class Hasher
    {
        public static int IterationCount => 10;

        public static int NumBytesRequested => 256 / 8;

        public static string HashPassword(string password, byte[] salt) =>
            Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password, salt: salt, prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: IterationCount, numBytesRequested: NumBytesRequested));
    }
}
