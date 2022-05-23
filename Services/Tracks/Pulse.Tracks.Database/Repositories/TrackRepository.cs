using Microsoft.EntityFrameworkCore;
using Pulse.Tracks.Core.Database;
using Pulse.Tracks.Core.Models;
using Pulse.Tracks.Core.Repositories;
using System.Linq.Expressions;

namespace Pulse.Tracks.Database.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly IDatabaseContext database;

        public TrackRepository(IDatabaseContext database) => this.database = database;

        public async Task Add(Track track)
        {
            await database.Tracks.AddAsync(track);
        }

        public async Task Delete(Track track)
        {
            await Task.Run(() => database.Tracks.Remove(track));
        }

        public async Task Delete(Guid trackId)
        {
            Track? track = await database.Tracks.FindAsync(trackId);

            if (track != null)
            {
                await Task.Run(() => database.Tracks.Remove(track));
            }
        }

        public async Task<Track?> Get(Guid trackId)
        {
            return await database.Tracks.FindAsync(trackId);
        }

        public async Task<IEnumerable<Track?>> GetList(Expression<Func<Track, bool>> predicate)
        {
            return await database.Tracks.Where(predicate).ToListAsync();
        }

        public async Task Update(Track track)
        {
            await Task.Run(() => database.Tracks.Update(track));
        }

        public async Task Save()
        {
            await database.SaveChangesAsync();
        }
    }
}
