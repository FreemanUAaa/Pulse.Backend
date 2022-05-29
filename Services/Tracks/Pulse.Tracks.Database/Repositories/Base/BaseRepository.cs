using Microsoft.EntityFrameworkCore;
using Pulse.Tracks.Core.Database;
using Pulse.Tracks.Core.Models.Base;
using System.Linq.Expressions;

namespace Pulse.Tracks.Database.Repositories.Base
{
    public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IDatabaseContext database;

        private readonly DbSet<TEntity> dbSet;

        public BaseRepository(IDatabaseContext database) => 
            (this.database, dbSet) = (database, database.Set<TEntity>());

        public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> predicate) => await dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<TEntity?>> GetList(Expression<Func<TEntity, bool>> predicate) => await dbSet.AsNoTracking().Where(predicate).ToListAsync();

        public async Task Add(TEntity entity) => await dbSet.AddAsync(entity);

        public async Task Delete(TEntity entity) => await Task.Run(() => dbSet.Remove(entity));

        public async Task Delete(Guid entityId)
        {
            TEntity? entity = await dbSet.FindAsync(entityId);

            if (entity != null)
            {
                await Task.Run(() => dbSet.Remove(entity));
            }
        }

        public async Task Update(TEntity entity) => await Task.Run(() => dbSet.Update(entity));

        public async Task Save() => await database.SaveChangesAsync();
    }
}
