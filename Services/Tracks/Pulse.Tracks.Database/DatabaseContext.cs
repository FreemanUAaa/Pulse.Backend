using Microsoft.EntityFrameworkCore;
using Pulse.Tracks.Core.Database;
using Pulse.Tracks.Core.Models;

namespace Pulse.Tracks.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Track> Tracks { get; set; } = null!;

        public DbSet<TrackSong> TrackSongs { get; set; } = null!;

        public DbSet<TrackLike> TrackLikes { get; set; } = null!;

        public DbSet<TrackCover> TrackCovers { get; set; } = null!;

        public DbSet<MusicType> MusicTypes { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}