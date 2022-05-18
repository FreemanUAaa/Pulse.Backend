using Microsoft.EntityFrameworkCore;
using Pulse.MusicTypes.Core.Models;

namespace Pulse.MusicTypes.Core.Database
{
    public interface IDatabaseContext
    {
        DbSet<MusicType> MusicTypes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
