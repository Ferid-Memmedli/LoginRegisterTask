using EntitiesLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using SharedLayer.DataAccess.Abstract;
using System.Linq.Expressions;

namespace SharedLayer.DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly DbSet<TEntity> dbSet;
        private readonly DbContext dbContext;
        public EfEntityRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<TEntity>();
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            //var addedEntity = dbContext.Entry(entity);
            //addedEntity.State = EntityState.Added;
            await dbSet.AddAsync(entity);
            return entity;
        }
        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate is not null ? await dbSet.AnyAsync(predicate) : await dbSet.AnyAsync();
        }
        public virtual async Task<int> CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate is not null ? await dbSet.CountAsync(predicate) : await dbSet.CountAsync();
        }
        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            // var deletedEntity = context.Entry(entity);
            // deletedEntity.State = EntityState.Deleted;
            await Task.Run(() => { dbSet.Remove(entity); });
            return entity;
        }
        public virtual async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
        }
        public virtual async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if (predicate is not null) query = query.Where(predicate);

            if (includeProperties is not null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.ToListAsync();
        }
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet.IgnoreQueryFilters();

            if (predicate is not null) query = query.Where(predicate);

            if (includeProperties is not null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.SingleOrDefaultAsync();
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool isTracking = false,
                   params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            query = isTracking ? query : query.AsNoTracking();

            if (predicate is not null)
                query = query.Where(predicate);

            if (orderBy is not null)
                query = orderBy(query);

            if (includeProperties is not null)
            {
                includeProperties.ToList().ForEach(include =>
                {
                    if (include != null)
                        query = query.Include(include);
                });
            }

            return query;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            //var a = dbSet.Attach(entity);
            //var updatedEntity = context.Entry(entity);
            //updatedEntity.State = EntityState.Modified;
            await Task.Run(() => { dbSet.Update(entity); });
            return entity;
        }
    }
}
