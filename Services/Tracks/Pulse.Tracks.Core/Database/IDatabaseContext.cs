using Microsoft.EntityFrameworkCore;
using Pulse.Tracks.Core.Models;

namespace Pulse.Tracks.Core.Database
{
    public interface IDatabaseContext
    {
        DbSet<Track> Tracks { get; set; }

        DbSet<TrackFile> TrackFiles { get; set; }

        DbSet<TrackLike> TrackLikes { get; set; }

        DbSet<TrackCover> TrackCovers { get; set; }

        DbSet<MusicType> MusicTypes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
