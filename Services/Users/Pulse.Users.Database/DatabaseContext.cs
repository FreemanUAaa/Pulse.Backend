using Microsoft.EntityFrameworkCore;
using Pulse.Users.Core.Database;
using Pulse.Users.Core.Models;

namespace Pulse.Users.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Cover> Covers { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
