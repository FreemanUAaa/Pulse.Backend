using Microsoft.EntityFrameworkCore;
using Pulse.Users.Core.Database;
using Pulse.Users.Database;
using System;

namespace Pulse.Users.Tests.Helpers.Databases
{
    public static class DatabaseContextFactory
    {
        public static IDatabaseContext Create()
        {
            DbContextOptions<DatabaseContext> options =
                new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            DatabaseContext context = new(options);

            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy(DatabaseContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
