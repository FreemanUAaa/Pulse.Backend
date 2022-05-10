using System.Security.Cryptography;

namespace Pulse.Users.Core.Helpers.Salts
{
    public static class Salt
    {
        public static int SaltSize => 128 / 8;

        public static byte[] Generate()
        {
            byte[] salt = new byte[SaltSize];
            RandomNumberGenerator generator = RandomNumberGenerator.Create();
            generator.GetNonZeroBytes(salt);

            return salt;
        }
    }
}
