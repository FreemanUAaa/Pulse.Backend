using Microsoft.EntityFrameworkCore;
using Pulse.Tracks.Core.Database;
using Pulse.Tracks.Core.Models;

namespace Pulse.Tracks.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Track> Tracks { get; set; }

        public DbSet<TrackFile> TrackFiles { get; set; }

        public DbSet<TrackLike> TrackLikes { get; set; }

        public DbSet<TrackCover> TrackCovers { get; set; }

        public DbSet<MusicType> MusicTypes { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) =>
            (Tracks, TrackFiles, TrackLikes, MusicTypes, TrackCovers) = (null!, null!, null!, null!, null!);
    }
}