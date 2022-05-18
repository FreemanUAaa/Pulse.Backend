using Microsoft.EntityFrameworkCore;
using Pulse.MusicTypes.Core.Database;
using Pulse.MusicTypes.Core.Models;

namespace Pulse.MusicTypes.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<MusicType> MusicTypes { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}