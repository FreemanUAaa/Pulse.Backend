using Microsoft.EntityFrameworkCore;

namespace Pulse.Tracks.Database
{
    public static class DatabaseInitializator
    {
        public static void Initializat(DbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
