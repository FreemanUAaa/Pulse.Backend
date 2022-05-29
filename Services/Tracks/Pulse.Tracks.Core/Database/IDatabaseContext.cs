using Microsoft.EntityFrameworkCore;
using Pulse.Tracks.Core.Models;

namespace Pulse.Tracks.Core.Database
{
    public interface IDatabaseContext
    {
        DbSet<Track> Tracks { get; set; }

        DbSet<TrackSong> TrackSongs { get; set; }

        DbSet<TrackLike> TrackLikes { get; set; }

        DbSet<TrackCover> TrackCovers { get; set; }

        DbSet<MusicType> MusicTypes { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
