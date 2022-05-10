using System;

namespace Pulse.Users.Core.Cache
{
    public static class CacheContracts
    {
        public static string GetUserKey(Guid userId) => $"user: {userId}";
    }
}
