using Microsoft.EntityFrameworkCore;
using Pulse.Users.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Pulse.Users.Core.Database
{
    public interface IDatabaseContext
    {
        DbSet<User> Users { get; set; }

        DbSet<Cover> Covers { get; set; }

        DbSet<MusicType> MusicTypes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
